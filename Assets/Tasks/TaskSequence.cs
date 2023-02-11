using System.Collections.Generic;
using Leopotam.EcsLite;

namespace PFY.Tasks
{
    public sealed class TaskSequence : Task
    {
        public override bool IsFinish => _isFinish;
        
        private bool _isRun;
        private bool _isFinish;
        private readonly List<Task> _tasks;
        
        public static Task Create(List<Task> tasks)
        {
            return new TaskSequence(tasks);
        }

        private TaskSequence(List<Task> tasks)
        {
            _isRun = false;
            _isFinish = false;
            _tasks = new List<Task>(tasks);
        }
        
        public override void Run(EcsWorld world)
        {
            _isRun = true;
            
            if (_tasks.Count > 0)
            {
                var task = _tasks[0];
                task.Run(world);
            }
        }

        public override void Update(EcsWorld world)
        {
            if (!_isRun || _isFinish)
            {
                return;
            }
            
            if (_tasks.Count > 0)
            {
                var task = _tasks[0];
                task.Update(world);

                if (task.IsFinish)
                {
                    _tasks.Remove(task);
                    task.Destroy();

                    if (_tasks.Count > 0)
                    {
                        task = _tasks[0];
                        task.Run(world);
                    }
                }
            }
            
            _isFinish = _tasks.Count <= 0;
        }

        public override void Destroy()
        {
            while (_tasks.Count > 0)
            {
                var task = _tasks[0];
                _tasks.Remove(task);
                task.Destroy();
            }

            _isFinish = true;
            _isRun = false;
        }
    }
}