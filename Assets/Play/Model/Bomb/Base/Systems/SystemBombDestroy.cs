using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Base.Systems
{
    using FilterBomb = EcsFilterInject
        <
            Inc
            <
                ComponentBombTag
            >
        >;
    
    public interface ISystemBombDestroy : IEcsDestroySystem { }

    public sealed class SystemBombDestroy : ISystemBombDestroy
    {
        private readonly FilterBomb _filterBomb = default;

        public static ISystemBombDestroy Create()
        {
            return new SystemBombDestroy();
        }
        
        private SystemBombDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterBomb.Value.DeleteAllEntitiesFromWorld();
        }
    }
}