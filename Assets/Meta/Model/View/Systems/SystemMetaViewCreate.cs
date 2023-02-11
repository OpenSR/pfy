using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Game.Settings.Scripts.Components;
using PFY.Meta.Model.View.Components;
using PFY.Meta.View;
using PFY.Tasks.Components;
using PFY.Utils;

namespace PFY.Meta.Model.View.Systems
{
    using FilterTasksService = EcsFilterInject
        <
            Inc
            <
                ComponentTasksService
            >
        >;
    
    using FilterGameSettings = EcsFilterInject
        <
            Inc
            <
                ComponentGameSettings
            >
        >;
    
    using PoolMetaView = EcsPoolInject
        <
            ComponentMetaView
        >;
    
    public interface ISystemMetaViewCreate : IEcsInitSystem { }

    public sealed class SystemMetaViewCreate : ISystemMetaViewCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterTasksService _filterTasksService = default;
        private readonly FilterGameSettings _filterGameSettings = default;
        private readonly PoolMetaView _poolMetaView = default;

        private MetaLayout _layout;

        public static ISystemMetaViewCreate Create(MetaLayout layout)
        {
            return new SystemMetaViewCreate(layout);
        }
        
        private SystemMetaViewCreate(MetaLayout layout)
        {
            _layout = layout;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var tasksService = _filterTasksService.Value.GetSingleComponent<ComponentTasksService>().TasksService;
            var gameSettings = _filterGameSettings.Value.GetSingleComponent<ComponentGameSettings>().GameSettings;
            _poolMetaView.Value.Add(_world.Value.NewEntity()).MetaView =
                MetaView.Create(_layout, tasksService, gameSettings);
            _layout = null;
        }
    }
}