using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Enemy.Components;
using PFY.Utils;

namespace PFY.Play.Model.Enemy.Systems
{
    using FilterPlayEnemy = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyTag
            >
        >;
    
    public interface ISystemPlayEnemiesDestroy : IEcsDestroySystem { }

    public sealed class SystemPlayEnemiesDestroy : ISystemPlayEnemiesDestroy
    {
        private readonly FilterPlayEnemy _filterPlayEnemy = default;
        
        public static ISystemPlayEnemiesDestroy Create()
        {
            return new SystemPlayEnemiesDestroy();
        }
        
        private SystemPlayEnemiesDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterPlayEnemy.Value.DeleteAllEntitiesFromWorld();
        }
    }
}