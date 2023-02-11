using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Commands.Components;
using PFY.Commands.Service;
using PFY.Utils;

namespace PFY.Commands.Systems
{
    using FilterCommandsService = EcsFilterInject
        <
            Inc
            <
                ComponentCommandsService
            >
        >;
    
    using PoolCommandsService = EcsPoolInject
        <
            ComponentCommandsService
        >;
    
    public interface ISystemCommandsServiceCreate : IEcsInitSystem { }

    public sealed class SystemCommandsServiceCreate : ISystemCommandsServiceCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterCommandsService _filterCommandsService = default;
        private readonly PoolCommandsService _poolCommandsService = default;

        public static ISystemCommandsServiceCreate Create()
        {
            return new SystemCommandsServiceCreate();
        }
        
        private SystemCommandsServiceCreate() { }

        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _filterCommandsService.Value.DeleteAllEntitiesFromWorld();
            var commandsServiceEntityId = _world.Value.NewEntity();
            _poolCommandsService.Value.Add(commandsServiceEntityId).CommandsService = CommandsService.Create();
        }
    }
}