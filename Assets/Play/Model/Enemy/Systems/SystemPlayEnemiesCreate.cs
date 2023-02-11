using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.Enemy.View;
using PFY.Level.Settings;
using PFY.Level.View;
using PFY.Play.Model.Enemy.Components;
using UnityEngine;

namespace PFY.Play.Model.Enemy.Systems
{
    using PoolPlayEnemy = EcsPoolInject
        <
            ComponentPlayEnemyTag
        >;
    
    using PoolPlayEnemyView = EcsPoolInject
        <
            ComponentPlayEnemyView
        >;
    
    using PoolPlayEnemyDamagePerceptionRadius = EcsPoolInject
        <
            ComponentPlayEnemyDamagePerceptionRadius
        >;
    
    public interface ISystemPlayEnemiesCreate : IEcsInitSystem { }

    public sealed class SystemPlayEnemiesCreate : ISystemPlayEnemiesCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly PoolPlayEnemy _poolPlayEnemy = default;
        private readonly PoolPlayEnemyView _poolPlayEnemyView = default;
        private readonly PoolPlayEnemyDamagePerceptionRadius _poolPlayEnemyDamagePerceptionRadius =
            default;

        private LevelLayout _levelLayout;
        private LevelSettings _levelSettings;
        
        public static ISystemPlayEnemiesCreate Create(LevelLayout levelLayout, LevelSettings levelSettings)
        {
            return new SystemPlayEnemiesCreate(levelLayout, levelSettings);
        }
        
        private SystemPlayEnemiesCreate(LevelLayout levelLayout, LevelSettings levelSettings)
        {
            _levelLayout = levelLayout;
            _levelSettings = levelSettings;
        }
        
        public void Init(IEcsSystems systems)
        {
            var spawnPointsList = _levelLayout.Spawns;
            var spawnPointCount = spawnPointsList.Count;
            var enemyCountToCreate = _levelSettings.enemyToCreateCount;
            var enemyPresetCount = _levelSettings.enemiesSettings.enemySettingsList.Count;
            var parent = _levelLayout.Ground.transform;

            for (var enemyIndex = 0; enemyIndex < enemyCountToCreate; enemyIndex++)
            {
                var enemyPreset = _levelSettings.enemiesSettings.enemySettingsList[Random.Range(0, enemyPresetCount)];
                var spawnPoint = spawnPointsList[Random.Range(0, spawnPointCount)];
                var enemyView = LevelEnemyView.Create(parent, enemyPreset.enemyPrefabReference, enemyPreset.moveSpeed, spawnPoint);
                var enemyEntity = _world.Value.NewEntity();
                _poolPlayEnemy.Value.Add(enemyEntity);
                _poolPlayEnemyView.Value.Add(enemyEntity).EnemyView = enemyView;
                _poolPlayEnemyDamagePerceptionRadius.Value.Add(enemyEntity).DamagePerceptionRadius =
                    enemyPreset.damagePerceptionRadius;
            }
            
            _levelLayout = null;
            _levelSettings = null;
        }
    }
}