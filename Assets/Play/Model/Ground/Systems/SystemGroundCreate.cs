using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Commands.Components;
using PFY.Level.Ground.View;
using PFY.Play.Model.Ground.Components;
using PFY.Utils;

namespace PFY.Play.Model.Ground.Systems
{
    using FilterCommandsService = EcsFilterInject
        <
            Inc
            <
                ComponentCommandsService
            >
        >;
    
    using PoolGround = EcsPoolInject
        <
            ComponentGroundTag
        >;
    
    using PoolGroundView = EcsPoolInject
        <
            ComponentGroundView
        >;
    
    public interface ISystemGroundCreate : IEcsInitSystem { }

    public sealed class SystemGroundCreate : ISystemGroundCreate
    {
        private readonly EcsWorldInject _world;
        private readonly FilterCommandsService _filterCommandsService = default;
        private readonly PoolGround _poolGround = default;
        private readonly PoolGroundView _poolGroundView = default;

        private GroundLayout _layout;

        public static ISystemGroundCreate Create(GroundLayout layout)
        {
            return new SystemGroundCreate(layout);
        }
        
        private SystemGroundCreate(GroundLayout layout)
        {
            _layout = layout;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var commandsService = _filterCommandsService.Value.GetSingleComponent<ComponentCommandsService>();
            var groundEntityId = _world.Value.NewEntity();
            _poolGround.Value.Add(groundEntityId);
            _poolGroundView.Value.Add(groundEntityId).GroundView = GroundView.Create(_layout, commandsService.CommandsService);
            _layout = null;
        }
    }
}