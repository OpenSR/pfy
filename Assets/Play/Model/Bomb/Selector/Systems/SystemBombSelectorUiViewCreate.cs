using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Commands.Components;
using PFY.Level.Bombs.BombSelector.UI.View;
using PFY.Level.Bombs.Settings.Scripts;
using PFY.Play.Model.Bomb.Selector.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Selector.Systems
{
    using FilterCommandsService = EcsFilterInject
        <
            Inc
            <
                ComponentCommandsService
            >
        >;
    
    using PoolBombSelectorUiView = EcsPoolInject
        <
            ComponentBombSelectorUiView
        >;
    
    public interface ISystemBombSelectorUiViewCreate : IEcsInitSystem { }

    public sealed class SystemBombSelectorUiViewCreate : ISystemBombSelectorUiViewCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterCommandsService _filterCommandsService = default;
        private readonly PoolBombSelectorUiView _poolBombSelectorUiView = default;

        private BombSelectorUiLayout _layout;
        private BombsSettings _bombsSettings;
        
        public static ISystemBombSelectorUiViewCreate Create(BombSelectorUiLayout layout, BombsSettings bombsSettings)
        {
            return new SystemBombSelectorUiViewCreate(layout, bombsSettings);
        }
        
        private SystemBombSelectorUiViewCreate(BombSelectorUiLayout layout, BombsSettings bombsSettings)
        {
            _layout = layout;
            _bombsSettings = bombsSettings;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var commandsService = _filterCommandsService.Value.GetSingleComponent<ComponentCommandsService>();
            _poolBombSelectorUiView.Value.Add(_world.Value.NewEntity()).BombSelectorUiView =
                BombSelectorUiView.Create(commandsService.CommandsService, _layout, _bombsSettings);
            _layout = null;
            _bombsSettings = null;
        }
    }
}