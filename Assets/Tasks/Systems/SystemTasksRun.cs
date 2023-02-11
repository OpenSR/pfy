using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Tasks.Components;
using PFY.Utils;

namespace PFY.Tasks.Systems
{
    using FilterTasksService = EcsFilterInject
        <
            Inc
            <
                ComponentTasksService
            >
        >;
    
    using FilterTasksCurrent = EcsFilterInject
        <
            Inc
            <
                ComponentTasksCurrent
            >
        >;
    
    using PoolTasksCurrent = EcsPoolInject
        <
            ComponentTasksCurrent
        >;
    
    public interface ISystemTasksRun : IEcsRunSystem { }

    public sealed class SystemTasksRun : ISystemTasksRun
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterTasksService _filterTasksService = default;
        private readonly FilterTasksCurrent _filterTasksCurrent = default;
        private readonly PoolTasksCurrent _poolTasksCurrent = default;

        public static ISystemTasksRun Create()
        {
            return new SystemTasksRun();
        }
        
        private SystemTasksRun() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterTasksCurrent.Value.GetEntitiesCount() > 0)
            {
                return;
            }

            var tasksService = _filterTasksService.Value.GetSingleComponent<ComponentTasksService>().TasksService;
            if (tasksService.TryDequeueQueue(out var task))
            {
                var tasksCurrentEntityId = _world.Value.NewEntity();
                _poolTasksCurrent.Value.Add(tasksCurrentEntityId).Task = task;
                task.Run(_world.Value);
            }
        }
    }
}