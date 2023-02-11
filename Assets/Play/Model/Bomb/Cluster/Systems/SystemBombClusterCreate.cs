﻿using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster;
using PFY.Level.Bombs.Bomb.Types.Cluster.View;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Bomb.Cluster.Components;
using PFY.Play.Model.Bomb.Drop.Components;
using PFY.Play.Model.Bomb.Types.Components;
using PFY.Play.Model.Ground.Components;
using PFY.Utils;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Cluster.Systems
{
    using FilterBombDrop = EcsFilterInject
            <
                Inc
                <
                    ComponentBombDropTag, 
                    ComponentBombDropTargetPosition
                >
            >;
        
    using FilterBombType = EcsFilterInject
        <
            Inc
            <
                ComponentBombTypeClusterTag, 
                ComponentBombTypeSelectedTag, 
                ComponentBombTypeSettings
            >
        >;
        
    using FilterGroundView = EcsFilterInject
        <
            Inc
            <
                ComponentGroundView
            >
        >;
    
    using PoolBombDropPosition = EcsPoolInject
        <
            ComponentBombDropTargetPosition
        >;
    
    using PoolBomb = EcsPoolInject
        <
            ComponentBombTag
        >;
    
    using PoolBombCluster = EcsPoolInject
    <
        ComponentBombClusterTag
    >;
    
    using PoolBombFallTimeAllMsec = EcsPoolInject
        <
            ComponentBombFallTimeAllMsec
        >;
    
    using PoolBombFallTimeCurrentMsec = EcsPoolInject
        <
            ComponentBombFallTimeCurrentMsec
        >;

    using PoolBombClusterFallTimeToSeparationMsec = EcsPoolInject
    <
        ComponentBombClusterFallTimeToSeparationMsec
    >;
    
    using PoolBombPositionStart = EcsPoolInject
        <
            ComponentBombPositionStart
        >;
    
    using PoolBombPositionTarget = EcsPoolInject
        <
            ComponentBombPositionTarget
        >;

    using PoolBombView = EcsPoolInject
        <
            ComponentBombView
        >;
    
    public interface ISystemBombClusterCreate : IEcsRunSystem { }

    public sealed class SystemBombClusterCreate : ISystemBombClusterCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterBombDrop _filterBombDrop = default;
        private readonly FilterBombType _filterBombType = default;
        private readonly FilterGroundView _filterGroundView = default;
        private readonly PoolBombDropPosition _poolBombDropPosition = default;
        private readonly PoolBomb _poolBomb = default;
        private readonly PoolBombCluster _poolBombCluster = default;
        private readonly PoolBombFallTimeAllMsec _poolBombFallTimeAllMsec = default;
        private readonly PoolBombClusterFallTimeToSeparationMsec _poolBombClusterFallTimeToSeparationMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombPositionStart _poolBombPositionStart = default;
        private readonly PoolBombPositionTarget _poolBombPositionTarget = default;
        private readonly PoolBombView _poolBombView = default;
        
        public static ISystemBombClusterCreate Create()
        {
            return new SystemBombClusterCreate();
        }
        
        private SystemBombClusterCreate() { }
        
        public void Run(IEcsSystems systems)
        {
            if (_filterBombType.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            var parent = _filterGroundView.Value.GetSingleComponent<ComponentGroundView>().GroundView.GroundTransform;
            var bombTypeSettings = _filterBombType.Value.GetSingleComponent<ComponentBombTypeSettings>().BombSettings;
            var bombTypeSpecificSettings = (ClusterBombSpecificSettings)bombTypeSettings.specificSettings;
            var bombFallTimeMsec = Mathf.Max(bombTypeSpecificSettings.fallTimeMsec, 1);
            const float bombFallDistanceUnit = 9f;
            
            foreach (var bombDropEntityId in _filterBombDrop.Value)
            {
                var targetPosition = _poolBombDropPosition.Value.Get(bombDropEntityId).TargetPosition;
                var startPosition = new Vector3(0f, bombFallDistanceUnit, 0f);
                var bombView = ClusterBombView.Create(bombTypeSettings.prefabReference, parent, startPosition);

                var bombEntityId = _world.Value.NewEntity();
                _poolBomb.Value.Add(bombEntityId);
                _poolBombCluster.Value.Add(bombEntityId);
                _poolBombFallTimeAllMsec.Value.Add(bombEntityId).Value = bombFallTimeMsec;
                _poolBombClusterFallTimeToSeparationMsec.Value.Add(bombEntityId).Value = bombTypeSpecificSettings.timeToSeparationMsec;
                _poolBombFallTimeCurrentMsec.Value.Add(bombEntityId).Value = 0;
                _poolBombPositionStart.Value.Add(bombEntityId).Value = startPosition;
                _poolBombPositionTarget.Value.Add(bombEntityId).Value = targetPosition;
                _poolBombView.Value.Add(bombEntityId).Value = bombView;
                
                _world.Value.DelEntity(bombDropEntityId);
            }
        }
    }
}