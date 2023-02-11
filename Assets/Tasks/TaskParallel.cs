using System.Collections.Generic;
using Leopotam.EcsLite;

namespace PFY.Tasks
{
    public sealed class TaskParallel : Task
    {
        public override bool IsFinish => _isFinish;

        private bool _isRun;
        private bool _isFinish;
        private readonly List<Task> _tasks;

        public static Task Create(List<Task> tasks)
        {
            return new TaskParallel(tasks);
        }

        private TaskParallel(List<Task> tasks)
        {
            _isRun = false;
            _isFinish = false;
            _tasks = new List<Task>(tasks);
        }
        
        public override void Run(EcsWorld world)
        {
            _isRun = true;
            foreach (var task in _tasks)
            {
                task.Run(world);
            }
        }

        public override void Update(EcsWorld world)
        {
            if (!_isRun || _isFinish)
            {
                return;
            }
            
            var destroy = new List<Task>();
            foreach (var task in _tasks)
            {
                task.Update(world);

                if (task.IsFinish)
                {
                    destroy.Add(task);
                }
            }

            while (destroy.Count > 0)
            {
                var task = destroy[0];
                destroy.Remove(task);
                _tasks.Remove(task);
                task.Destroy();
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