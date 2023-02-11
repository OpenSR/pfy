using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Loader.Components;
using PFY.Utils;

namespace PFY.Loader.Systems
{
    using FilterLoaderView = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderView
            >
        >;
    
    using FilterLoaderHide = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderHideTag
            >
        >;
    
    public interface ISystemLoaderHide : IEcsRunSystem { }
    
    public sealed class SystemLoaderHide : ISystemLoaderHide
    {
        private readonly FilterLoaderView _filterLoaderView = default;
        private readonly FilterLoaderHide _filterLoaderHide = default;

        public static ISystemLoaderHide Create()
        {
            return new SystemLoaderHide();
        }
        
        private SystemLoaderHide() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterLoaderHide.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            _filterLoaderHide.Value.DeleteAllEntitiesFromWorld();
            _filterLoaderView.Value.GetSingleComponent<ComponentLoaderView>().LoaderView.Hide();
        }
    }
}