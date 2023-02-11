using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster;
using PFY.Level.Bombs.Bomb.Types.Cluster.AttackUnit.View;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Bomb.Cluster.AttackUnit.Components;
using PFY.Play.Model.Bomb.Cluster.Components;
using PFY.Play.Model.Bomb.Types.Components;
using PFY.Play.Model.Ground.Components;
using PFY.Utils;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Cluster.Systems
{
    using FilterBombClusterActive = EcsFilterInject
        <
            Inc
            <
                ComponentBombClusterTag,
                ComponentBombClusterSeparatingTag,
                ComponentBombPositionStart,
                ComponentBombPositionTarget
            >
        >;
    
    using FilterBombType = EcsFilterInject
        <
            Inc
            <
                ComponentBombTypeClusterTag, 
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
    
    using PoolBomb = EcsPoolInject
        <
            ComponentBombTag
        >;
    
    using PoolBombClusterAttackUnit = EcsPoolInject
        <
            ComponentBombClusterAttackUnitTag
        >;

    using PoolBombFallTimeAllMsec = EcsPoolInject
        <
            ComponentBombFallTimeAllMsec
        >;
    
    using PoolBombFallTimeCurrentMsec = EcsPoolInject
        <
            ComponentBombFallTimeCurrentMsec
        >;
    
    using PoolBombPositionStart = EcsPoolInject
        <
            ComponentBombPositionStart
        >;
    
    using PoolBombPositionTarget = EcsPoolInject
        <
            ComponentBombPositionTarget
        >;
    
    using PoolBombDamageRadiusInUnit = EcsPoolInject
        <
            ComponentBombDamageRadiusInUnit
        >;
    
    using PoolBombView = EcsPoolInject
        <
            ComponentBombView
        >;

    public interface ISystemBombClusterSeparating : IEcsRunSystem { }

    public sealed class SystemBombClusterSeparating : ISystemBombClusterSeparating
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterBombClusterActive _filterBombClusterActive = default;
        private readonly FilterBombType _filterBombType = default;
        private readonly FilterGroundView _filterGroundView = default;
        private readonly PoolBomb _poolBomb = default;
        private readonly PoolBombClusterAttackUnit _poolBombClusterAttackUnit = default;
        private readonly PoolBombFallTimeAllMsec _poolBombFallTimeAllMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombPositionStart _poolBombPositionStart = default;
        private readonly PoolBombPositionTarget _poolBombPositionTarget = default;
        private readonly PoolBombDamageRadiusInUnit _poolBombDamageRadiusInUnit = default;
        private readonly PoolBombView _poolBombView = default;

        public static ISystemBombClusterSeparating Create()
        {
            return new SystemBombClusterSeparating();
        }

        private SystemBombClusterSeparating() { }
        
        public void Run(IEcsSystems systems)
        {
            if (_filterBombClusterActive.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            var parent = _filterGroundView.Value.GetSingleComponent<ComponentGroundView>().GroundView.GroundTransform;
            var bombTypeSettings = _filterBombType.Value.GetSingleComponent<ComponentBombTypeSettings>().BombSettings;
            var bombTypeSpecificSettings = (ClusterBombSpecificSettings)bombTypeSettings.specificSettings;
            var attackUnitSettings = bombTypeSpecificSettings.attackUnitSettings;
            var fallTimeAll = bombTypeSpecificSettings.fallTimeMsec - bombTypeSpecificSettings.timeToSeparationMsec;
            var t = bombTypeSpecificSettings.timeToSeparationMsec / (float)bombTypeSpecificSettings.fallTimeMsec;
            
            foreach (var bombClusterEntityId in _filterBombClusterActive.Value)
            {
                var positionStart = _poolBombPositionStart.Value.Get(bombClusterEntityId).Value;
                var positionTarget = _poolBombPositionTarget.Value.Get(bombClusterEntityId).Value;
                positionStart = Vector3.Lerp(positionStart, positionTarget, t);

                foreach (var attackTargetDeltaPosition in attackUnitSettings.attackUnitTargetDeltaPosition)
                {
                    var bombView = ClusterBombAttackUnitView.Create(attackUnitSettings.prefabReference, parent, positionStart);
                    var newPositionTarget = new Vector3(positionTarget.x + attackTargetDeltaPosition.dx, positionTarget.y, positionTarget.z + attackTargetDeltaPosition.dz);
                    var bombEntityId = _world.Value.NewEntity();
                    _poolBomb.Value.Add(bombEntityId);
                    _poolBombClusterAttackUnit.Value.Add(bombEntityId);
                    _poolBombFallTimeAllMsec.Value.Add(bombEntityId).Value = fallTimeAll;
                    _poolBombFallTimeCurrentMsec.Value.Add(bombEntityId).Value = 0;
                    _poolBombPositionStart.Value.Add(bombEntityId).Value = positionStart;
                    _poolBombPositionTarget.Value.Add(bombEntityId).Value = newPositionTarget;
                    _poolBombDamageRadiusInUnit.Value.Add(bombEntityId).Value = attackUnitSettings.attackUnitDamageRadius;
                    _poolBombView.Value.Add(bombEntityId).Value = bombView;
                }
            }
        }
    }
}