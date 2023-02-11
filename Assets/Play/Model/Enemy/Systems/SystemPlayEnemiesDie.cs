using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Enemy.Components;
using PFY.Utils;

namespace PFY.Play.Model.Enemy.Systems
{
    using FilterEnemyDie = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyDieTag
            >
        >;

    public interface ISystemPlayEnemiesDie : IEcsRunSystem { }

    public sealed class SystemPlayEnemiesDie : ISystemPlayEnemiesDie
    {
        private readonly FilterEnemyDie _filterEnemyDie = default;
        
        public static SystemPlayEnemiesDie Create()
        {
            return new SystemPlayEnemiesDie();
        }
        
        private SystemPlayEnemiesDie() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            _filterEnemyDie.Value.DeleteAllEntitiesFromWorld();
        }
    }
}