namespace PFY.Tasks.Service
{
    public interface ITasksService
    {
        void Enqueue(Task task);
        bool TryDequeueQueue(out Task task);
        void Destroy();
    }
}