using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Selector.Components;
using PFY.Play.Model.Bomb.Ui.Selected.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Ui.Selected.Systems
{
    using FilterBombSelectorUiView = EcsFilterInject
        <
            Inc
            <
                ComponentBombSelectorUiView
            >
        >;
    
    using FilterBombUiSelected = EcsFilterInject
        <
            Inc
            <
                ComponentBombUiSelectedTag,
                ComponentBombUiSelectedId
            >
        >;
    
    using PoolBombUiSelectedId = EcsPoolInject
        <
            ComponentBombUiSelectedId
        >;
    
    public interface ISystemBombUiSelectedUpdate : IEcsRunSystem { }

    public sealed class SystemBombUiSelectedUpdate : ISystemBombUiSelectedUpdate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterBombSelectorUiView _filterBombSelectorUiView = default;
        private readonly FilterBombUiSelected _filterBombUiSelected = default;
        private readonly PoolBombUiSelectedId _poolBombUiSelectedId = default;
        
        public static ISystemBombUiSelectedUpdate Create()
        {
            return new SystemBombUiSelectedUpdate();
        }
        
        private SystemBombUiSelectedUpdate() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var bombSelectorUiView = _filterBombSelectorUiView.Value.GetSingleComponent<ComponentBombSelectorUiView>();
            
            foreach (var bombUiSelectedEntityId in _filterBombUiSelected.Value)
            {
                bombSelectorUiView.BombSelectorUiView.SelectBomb(_poolBombUiSelectedId.Value.Get(bombUiSelectedEntityId).Id);
                _world.Value.DelEntity(bombUiSelectedEntityId);
            }
        }
    }
}