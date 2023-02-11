using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Enemy.Components;

namespace PFY.Play.Model.Enemy.Systems
{
    using FilterPlayEnemy = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyTag, 
                ComponentPlayEnemyView
            >, 
            Exc
            <
                ComponentPlayEnemyIsActiveTag
            >
        >;
    
    using PoolPlayEnemyIsActive = EcsPoolInject
        <
            ComponentPlayEnemyIsActiveTag
        >;
    
    using PoolPlayEnemyView = EcsPoolInject
        <
            ComponentPlayEnemyView
        >;
    
    public interface ISystemPlayEnemiesCheckIsActive : IEcsRunSystem { }

    public sealed class SystemPlayEnemiesCheckIsActive : ISystemPlayEnemiesCheckIsActive
    {
        private readonly FilterPlayEnemy _filterPlayEnemy = default;
        private readonly PoolPlayEnemyIsActive _poolPlayEnemyIsActive = default;
        private readonly PoolPlayEnemyView _poolPlayEnemyView = default;
        
        public static ISystemPlayEnemiesCheckIsActive Create()
        {
            return new SystemPlayEnemiesCheckIsActive();
        }
        
        private SystemPlayEnemiesCheckIsActive() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var enemyEntityId in _filterPlayEnemy.Value)
            {
                if (_poolPlayEnemyView.Value.Get(enemyEntityId).EnemyView.IsActive)
                {
                    _poolPlayEnemyIsActive.Value.Add(enemyEntityId);
                }
            }
        }
    }
}