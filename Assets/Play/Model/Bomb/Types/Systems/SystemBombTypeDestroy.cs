using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Types.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Types.Systems
{
    using FilterBombType = EcsFilterInject
        <
            Inc
            <
                ComponentBombTypeTag
            >
        >;
    
    public interface ISystemBombTypeDestroy : IEcsDestroySystem { }

    public sealed class SystemBombTypeDestroy : ISystemBombTypeDestroy
    {
        private readonly FilterBombType _filterBombType = default;

        public static ISystemBombTypeDestroy Create()
        {
            return new SystemBombTypeDestroy();
        }
        
        private SystemBombTypeDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterBombType.Value.DeleteAllEntitiesFromWorld();
        }
    }
}