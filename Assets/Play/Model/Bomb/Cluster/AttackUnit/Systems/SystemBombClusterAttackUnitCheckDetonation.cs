using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Bomb.Cluster.AttackUnit.Components;

namespace PFY.Play.Model.Bomb.Cluster.AttackUnit.Systems
{
    using FilterBombClusterAttackUnitActive = EcsFilterInject
        <
            Inc
            <
                ComponentBombClusterAttackUnitTag,
                ComponentBombIsActiveTag, 
                ComponentBombFallTimeAllMsec, 
                ComponentBombFallTimeCurrentMsec,
                ComponentBombPositionTarget
            >
        >;
    
    using PoolBombIsActive = EcsPoolInject
        <
            ComponentBombIsActiveTag
        >;
    
    using PoolBombFallTimeAllMsec = EcsPoolInject
        <
            ComponentBombFallTimeAllMsec
        >;
    
    using PoolBombFallTimeCurrentMsec = EcsPoolInject
        <
            ComponentBombFallTimeCurrentMsec
        >;

    using PoolBombPositionTarget = EcsPoolInject
        <
            ComponentBombPositionTarget
        >;
    
    using PoolBombDetonation = EcsPoolInject
        <
            ComponentBombDetonationTag
        >;
    
    using PoolBombDetonationPosition = EcsPoolInject
        <
            ComponentBombDetonationPosition
        >;
    
    public interface ISystemBombClusterAttackUnitCheckDetonation : IEcsRunSystem { }

    public sealed class SystemBombClusterAttackUnitCheckDetonation : ISystemBombClusterAttackUnitCheckDetonation
    {
        private readonly FilterBombClusterAttackUnitActive _BombClusterAttackUnitActive = default;
        private readonly PoolBombIsActive _poolBombIsActive = default;
        private readonly PoolBombFallTimeAllMsec _poolBombFallTimeAllMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombPositionTarget _poolBombPositionTarget = default;
        private readonly PoolBombDetonation _poolBombDetonation = default;
        private readonly PoolBombDetonationPosition _poolBombDetonationPosition = default;
        
        public static ISystemBombClusterAttackUnitCheckDetonation Create()
        {
            return new SystemBombClusterAttackUnitCheckDetonation();
        }
        
        private SystemBombClusterAttackUnitCheckDetonation() { }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var bombClusterAttackUnitEntityId in _BombClusterAttackUnitActive.Value)
            {
                var fallTimeAllMsec = _poolBombFallTimeAllMsec.Value.Get(bombClusterAttackUnitEntityId).Value;
                var fallTimeCurrentMsec = _poolBombFallTimeCurrentMsec.Value.Get(bombClusterAttackUnitEntityId).Value;
                var positionTarget = _poolBombPositionTarget.Value.Get(bombClusterAttackUnitEntityId).Value;
                
                if (fallTimeCurrentMsec >= fallTimeAllMsec)
                {
                    _poolBombIsActive.Value.Del(bombClusterAttackUnitEntityId);
                    _poolBombDetonation.Value.Add(bombClusterAttackUnitEntityId);
                    _poolBombDetonationPosition.Value.Add(bombClusterAttackUnitEntityId).Value = positionTarget;
                }
            }
        }
    }
}