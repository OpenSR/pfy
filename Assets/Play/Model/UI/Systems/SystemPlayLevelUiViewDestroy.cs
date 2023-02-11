using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.UI.Components;
using PFY.Utils;

namespace PFY.Play.Model.UI.Systems
{
    using FilterPlayLevelUiView = EcsFilterInject
        <
            Inc
            <
                ComponentPlayLevelUiView
            >
        >;
    
    public interface ISystemPlayLevelUiViewDestroy : IEcsDestroySystem { }

    public sealed class SystemPlayLevelUiViewDestroy : ISystemPlayLevelUiViewDestroy
    {
        private readonly FilterPlayLevelUiView _filterPlayLevelUiView = default;
        
        public static ISystemPlayLevelUiViewDestroy Create()
        {
            return new SystemPlayLevelUiViewDestroy();
        }
        
        private SystemPlayLevelUiViewDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterPlayLevelUiView.Value.DeleteAllEntitiesFromWorld();
        }
    }
}