using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Enemy.Components;
using PFY.Play.Model.Layout.Components;
using PFY.Utils;
using UnityEngine;

namespace PFY.Play.Model.Enemy.Systems
{
    using FilterPlayEnemy = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyTag, 
                ComponentPlayEnemyIsActiveTag, 
                ComponentPlayEnemyView
            >
        >;
    
    using FilterLevelLayout = EcsFilterInject
        <
            Inc
            <
                ComponentPlayLevelLayout
            >
        >;
    
    using PoolPlayEnemyView = EcsPoolInject
        <
            ComponentPlayEnemyView
        >;
    
    public interface ISystemPlayEnemiesUpdateNavigation: IEcsRunSystem { }

    public sealed class SystemPlayEnemiesUpdateNavigation : ISystemPlayEnemiesUpdateNavigation
    {
        private readonly FilterPlayEnemy _filterPlayEnemy = default;
        private readonly FilterLevelLayout _filterLevelLayout = default;
        private readonly PoolPlayEnemyView _poolPlayEnemyView = default;
        
        public static ISystemPlayEnemiesUpdateNavigation Create()
        {
            return new SystemPlayEnemiesUpdateNavigation();
        }
        
        private SystemPlayEnemiesUpdateNavigation() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var waypointList = _filterLevelLayout.Value.GetSingleComponent<ComponentPlayLevelLayout>().LevelLayout
                .Waypoints;
            var waypointCount = waypointList.Count;

            foreach (var enemyEntityId in _filterPlayEnemy.Value)
            {
                var navigation = _poolPlayEnemyView.Value.Get(enemyEntityId).EnemyView.Navigation;
                if (!navigation.pathPending && navigation.remainingDistance < 0.5f)
                {
                    navigation.destination = waypointList[Random.Range(0, waypointCount)];
                }
            }
        }
    }
}