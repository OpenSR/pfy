using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Enemy.Components;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Base.Systems
{
    using FilterBombDetonation = EcsFilterInject
        <
            Inc
            <
                ComponentBombDetonationTag,
                ComponentBombDetonationPosition,
                ComponentBombDamageRadiusInUnit
            >
        >;
    
    using FilterEnemy = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyTag, 
                ComponentPlayEnemyIsActiveTag, 
                ComponentPlayEnemyView
            >
        >;
    
    using PoolBombDetonationPosition = EcsPoolInject
        <
            ComponentBombDetonationPosition
        >;
    
    using PoolBombDamageRadiusInUnit = EcsPoolInject
    <
        ComponentBombDamageRadiusInUnit
    >;
    
    using PoolEnemyView = EcsPoolInject
        <
            ComponentPlayEnemyView
        >;
    
    using PoolEnemyIsActive = EcsPoolInject
    <
        ComponentPlayEnemyIsActiveTag
    >;
    
    using PoolEnemyDie = EcsPoolInject
    <
        ComponentPlayEnemyDieTag
    >;

    public interface ISystemBombDetonationTakeDamage : IEcsRunSystem { }

    public sealed class SystemBombDetonationTakeDamage : ISystemBombDetonationTakeDamage
    {
        private const int LayerMask = 1 << 6;

        private readonly FilterBombDetonation _filterBombDetonation = default;
        private readonly FilterEnemy _filterEnemy = default;
        private readonly PoolBombDamageRadiusInUnit _poolBombDamageRadiusInUnit = default;
        private readonly PoolBombDetonationPosition _poolBombDetonationPosition = default;
        private readonly PoolEnemyView _poolEnemyView = default;
        private readonly PoolEnemyIsActive _poolEnemyIsActive = default;
        private readonly PoolEnemyDie _poolEnemyDie = default;
        
        public static ISystemBombDetonationTakeDamage Create()
        {
            return new SystemBombDetonationTakeDamage();
        }
        
        private SystemBombDetonationTakeDamage() { }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var bombDetonationEntityId in _filterBombDetonation.Value)
            {
                var detonationPosition = _poolBombDetonationPosition.Value.Get(bombDetonationEntityId).Value;
                var damageRadiusInUnit = _poolBombDamageRadiusInUnit.Value.Get(bombDetonationEntityId).Value;

                foreach (var enemyEntityId in _filterEnemy.Value)
                {
                    var enemyPosition = _poolEnemyView.Value.Get(enemyEntityId).EnemyView.Position;
                    var vector = enemyPosition - detonationPosition;

                    if (vector.magnitude > damageRadiusInUnit)
                    {
                        continue;
                    }

                    if (!Physics.Raycast(detonationPosition, vector.normalized, damageRadiusInUnit, LayerMask))
                    {
                        _poolEnemyIsActive.Value.Del(enemyEntityId);
                        _poolEnemyDie.Value.Add(enemyEntityId);
                    }
                }
            }
        }
    }
}