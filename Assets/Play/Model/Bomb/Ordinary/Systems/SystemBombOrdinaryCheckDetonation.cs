using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Bomb.Ordinary.Components;

namespace PFY.Play.Model.Bomb.Ordinary.Systems
{
    using FilterBombOrdinaryActive = EcsFilterInject
    <
        Inc
        <
            ComponentBombOrdinaryTag,
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
    
    public interface ISystemBombOrdinaryCheckDetonation : IEcsRunSystem { }

    public sealed class SystemBombOrdinaryCheckDetonation : ISystemBombOrdinaryCheckDetonation
    {
        private readonly FilterBombOrdinaryActive _filterBombOrdinaryActive = default;
        private readonly PoolBombIsActive _poolBombIsActive = default;
        private readonly PoolBombFallTimeAllMsec _poolBombFallTimeAllMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombPositionTarget _poolBombPositionTarget = default;
        private readonly PoolBombDetonation _poolBombDetonation = default;
        private readonly PoolBombDetonationPosition _poolBombDetonationPosition = default;
        
        public static ISystemBombOrdinaryCheckDetonation Create()
        {
            return new SystemBombOrdinaryCheckDetonation();
        }
        
        private SystemBombOrdinaryCheckDetonation() { }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var bombOrdinaryEntityId in _filterBombOrdinaryActive.Value)
            {
                var fallTimeAllMsec = _poolBombFallTimeAllMsec.Value.Get(bombOrdinaryEntityId).Value;
                var fallTimeCurrentMsec = _poolBombFallTimeCurrentMsec.Value.Get(bombOrdinaryEntityId).Value;
                var positionTarget = _poolBombPositionTarget.Value.Get(bombOrdinaryEntityId).Value;
                
                if (fallTimeCurrentMsec >= fallTimeAllMsec)
                {
                    _poolBombIsActive.Value.Del(bombOrdinaryEntityId);
                    _poolBombDetonation.Value.Add(bombOrdinaryEntityId);
                    _poolBombDetonationPosition.Value.Add(bombOrdinaryEntityId).Value = positionTarget;
                }
            }
        }
    }
}