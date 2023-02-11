using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Loader.Components;
using PFY.Loader.View;
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
    
    using PoolLoaderView = EcsPoolInject
        <
            ComponentLoaderView
        >;
    
    public interface ISystemLoaderViewCreate : IEcsInitSystem { }

    public sealed class SystemLoaderViewCreate : ISystemLoaderViewCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterLoaderView _filterLoaderView = default;
        private readonly PoolLoaderView _poolLoaderView = default;

        private LoaderLayout _loaderLayout;

        public static ISystemLoaderViewCreate Create(LoaderLayout loaderLayout)
        {
            return new SystemLoaderViewCreate(loaderLayout);
        }

        private SystemLoaderViewCreate(LoaderLayout loaderLayout)
        {
            _loaderLayout = loaderLayout;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _filterLoaderView.Value.DeleteAllEntitiesFromWorld();
            var entityId = _world.Value.NewEntity();
            _poolLoaderView.Value.Add(entityId).LoaderView = LoaderView.Create(_loaderLayout);
            _loaderLayout = null;
        }
    }
}