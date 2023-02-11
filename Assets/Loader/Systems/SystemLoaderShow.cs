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
    
    using FilterLoaderShow = EcsFilterInject
        <
            Inc
            <
                ComponentLoaderShowTag
            >
        >;
    
    public interface ISystemLoaderShow : IEcsRunSystem { }

    public sealed class SystemLoaderShow : ISystemLoaderShow
    {
        private readonly FilterLoaderView _filterLoaderView = default;
        private readonly FilterLoaderShow _filterLoaderShow = default;

        public static ISystemLoaderShow Create()
        {
            return new SystemLoaderShow();
        }
        
        private SystemLoaderShow() { }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterLoaderShow.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            _filterLoaderShow.Value.DeleteAllEntitiesFromWorld();
            _filterLoaderView.Value.GetSingleComponent<ComponentLoaderView>().LoaderView.Show();
        }
    }
}