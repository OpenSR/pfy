using System.Collections.Generic;

namespace PFY.Tasks.Service
{
    public sealed class TasksService : ITasksService
    {
        private readonly Queue<Task> _tasks;

        public static ITasksService Create()
        {
            return new TasksService();
        }

        private TasksService()
        {
            _tasks = new Queue<Task>();
        }

        void ITasksService.Enqueue(Task task)
        {
            _tasks.Enqueue(task);
        }
        
        bool ITasksService.TryDequeueQueue(out Task task)
        {
            return _tasks.TryDequeue(out task);
        }

        void ITasksService.Destroy()
        {
            _tasks.Clear();
        }
    }
}