using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Selector.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Selector.Systems
{
    using FilterBombSelectorUiView = EcsFilterInject
        <
            Inc
            <
                ComponentBombSelectorUiView
            >
        >;
    
    public interface ISystemBombSelectorUiViewDestroy : IEcsDestroySystem { }

    public sealed class SystemBombSelectorUiViewDestroy : ISystemBombSelectorUiViewDestroy
    {
        private readonly FilterBombSelectorUiView _filterBombSelectorUiView = default;

        public static ISystemBombSelectorUiViewDestroy Create()
        {
            return new SystemBombSelectorUiViewDestroy();
        }

        private SystemBombSelectorUiViewDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterBombSelectorUiView.Value.DeleteAllEntitiesFromWorld();
        }
    }
}