using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Loader.Components;

namespace PFY.Loader.Systems
{
    using FilterLoaderView = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderView
            >
        >;
    
    using FilterLoaderUpdateProgress = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderUpdateProgress
            >
        >;
    
    using PoolLoaderView = EcsPoolInject
        <
            ComponentLoaderView
        >;
    
    using PoolLoaderUpdateProgress = EcsPoolInject
        <
            ComponentLoaderUpdateProgress
        >;
    
    public interface ISystemLoaderUpdateProgress : IEcsRunSystem { }

    public sealed class SystemLoaderUpdateProgress : ISystemLoaderUpdateProgress
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterLoaderView _filterLoaderView = default;
        private readonly FilterLoaderUpdateProgress _filterLoaderUpdateProgress = default;
        private readonly PoolLoaderView _poolLoaderView = default;
        private readonly PoolLoaderUpdateProgress _poolLoaderUpdateProgress = default;

        public static ISystemLoaderUpdateProgress Create()
        {
            return new SystemLoaderUpdateProgress();
        }

        private SystemLoaderUpdateProgress() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var loaderUpdateProgressEntityId in _filterLoaderUpdateProgress.Value)
            {
                foreach (var loaderViewEntityId in _filterLoaderView.Value)
                {
                    _poolLoaderView.Value.Get(loaderViewEntityId).LoaderView
                        .UpdateProgress(_poolLoaderUpdateProgress.Value.Get(loaderUpdateProgressEntityId).Progress);
                }
                
                _world.Value.DelEntity(loaderUpdateProgressEntityId);
            }
        }
    }
}