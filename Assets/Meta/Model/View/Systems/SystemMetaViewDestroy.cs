using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Meta.Model.View.Components;
using PFY.Utils;

namespace PFY.Meta.Model.View.Systems
{
    using FilterMetaView = EcsFilterInject<Inc<ComponentMetaView>>;
    
    public interface ISystemMetaViewDestroy : IEcsDestroySystem { }

    public sealed class SystemMetaViewDestroy : ISystemMetaViewDestroy
    {
        private readonly FilterMetaView _filterMetaView = default;

        public static ISystemMetaViewDestroy Create()
        {
            return new SystemMetaViewDestroy();
        }
        
        private SystemMetaViewDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterMetaView.Value.DeleteAllEntitiesFromWorld();
        }
    }
}