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
    
    using FilterLoaderUpdateHeader = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderUpdateHeader
            >
        >;
    
    using PoolLoaderUpdateHeader = EcsPoolInject
        <
            ComponentLoaderUpdateHeader
        >;
    
    public interface ISystemLoaderUpdateHeader : IEcsRunSystem { }

    public sealed class SystemLoaderUpdateHeader : ISystemLoaderUpdateHeader
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterLoaderView _filterLoaderView = default;
        private readonly FilterLoaderUpdateHeader _filterLoaderUpdateHeader = default;
        private readonly PoolLoaderUpdateHeader _poolLoaderUpdateHeader = default;

        public static ISystemLoaderUpdateHeader Create()
        {
            return new SystemLoaderUpdateHeader();
        }

        private SystemLoaderUpdateHeader() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterLoaderUpdateHeader.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            var loaderView = _filterLoaderView.Value.GetSingleComponent<ComponentLoaderView>().LoaderView;
            
            foreach (var loaderUpdateHeaderEntityId in _filterLoaderUpdateHeader.Value)
            {
                loaderView.UpdateHeader(_poolLoaderUpdateHeader.Value.Get(loaderUpdateHeaderEntityId).Text);
                _world.Value.DelEntity(loaderUpdateHeaderEntityId);
            }
        }
    }
}