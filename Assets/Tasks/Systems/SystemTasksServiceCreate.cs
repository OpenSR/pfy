using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Tasks.Components;
using PFY.Tasks.Service;
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
    
    using PoolTasksService = EcsPoolInject
        <
            ComponentTasksService
        >;
    
    public interface ISystemTasksServiceCreate : IEcsInitSystem { }

    public sealed class SystemTasksServiceCreate : ISystemTasksServiceCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterTasksService _filterTasksService = default;
        private readonly PoolTasksService _poolTasksService = default;

        public static ISystemTasksServiceCreate Create()
        {
            return new SystemTasksServiceCreate();
        }
        
        private SystemTasksServiceCreate() { }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _filterTasksService.Value.DeleteAllEntitiesFromWorld();
            var commandsServiceEntityId = _world.Value.NewEntity();
            _poolTasksService.Value.Add(commandsServiceEntityId).TasksService = TasksService.Create();
        }
    }
}