using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Utils;

namespace PFY.Play.Model.Bomb.Base.Systems
{
    using FilterBombDetonation = EcsFilterInject
        <
            Inc
            <
                ComponentBombDetonationTag
            >
        >;

    public interface ISystemBombDetonationDestroy: IEcsRunSystem { }

    public sealed class SystemBombDetonationDestroy : ISystemBombDetonationDestroy
    {
        private readonly FilterBombDetonation _filterBombDetonation = default;
        
        public static ISystemBombDetonationDestroy Create()
        {
            return new SystemBombDetonationDestroy();
        }
        
        private SystemBombDetonationDestroy() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            _filterBombDetonation.Value.DeleteAllEntitiesFromWorld();
        }
    }
}