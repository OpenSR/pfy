using Leopotam.EcsLite;
using PFY.Tasks.Service;

namespace PFY.Tasks.Components
{
    public struct ComponentTasksService : IEcsAutoReset<ComponentTasksService>
    {
        public ITasksService TasksService;
        
        public void AutoReset(ref ComponentTasksService c)
        {
            c.TasksService?.Destroy();
            c.TasksService = null;
        }
    }
}