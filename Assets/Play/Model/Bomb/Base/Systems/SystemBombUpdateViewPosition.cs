using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Base.Systems
{
    using FilterBombOrdinaryActive = EcsFilterInject
        <
            Inc
            <
                ComponentBombTag,
                ComponentBombIsActiveTag, 
                ComponentBombFallTimeAllMsec, 
                ComponentBombFallTimeCurrentMsec, 
                ComponentBombPositionStart, 
                ComponentBombPositionTarget, 
                ComponentBombView
            >
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
    
    using PoolBombView = EcsPoolInject
        <
            ComponentBombView
        >;

    public interface ISystemBombUpdateViewPosition : IEcsRunSystem { }

    public sealed class SystemBombUpdateViewPosition : ISystemBombUpdateViewPosition
    {
        private readonly FilterBombOrdinaryActive _filterBombOrdinaryActive = default;
        private readonly PoolBombFallTimeAllMsec _poolBombFallTimeAllMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombPositionStart _poolBombPositionStart = default;
        private readonly PoolBombPositionTarget _poolBombPositionTarget = default;
        private readonly PoolBombView _poolBombView = default;
        
        public static ISystemBombUpdateViewPosition Create()
        {
            return new SystemBombUpdateViewPosition();
        }
        
        private SystemBombUpdateViewPosition() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var dtMsec = (int)(Time.deltaTime * 1000);

            foreach (var bombOrdinaryEntityId in _filterBombOrdinaryActive.Value)
            {
                var fallTimeAllMsec = Mathf.Max(_poolBombFallTimeAllMsec.Value.Get(bombOrdinaryEntityId).Value, 1);
                var fallTimeCurrentMsec = _poolBombFallTimeCurrentMsec.Value.Get(bombOrdinaryEntityId).Value;
                var positionStart = _poolBombPositionStart.Value.Get(bombOrdinaryEntityId).Value;
                var positionTarget = _poolBombPositionTarget.Value.Get(bombOrdinaryEntityId).Value;
                var view = _poolBombView.Value.Get(bombOrdinaryEntityId).Value;

                fallTimeCurrentMsec += dtMsec;
                _poolBombFallTimeCurrentMsec.Value.Get(bombOrdinaryEntityId).Value = fallTimeCurrentMsec;

                var t = Mathf.Min(1f, fallTimeCurrentMsec / (float)fallTimeAllMsec);
                var newPosition = Vector3.Lerp(positionStart, positionTarget, t);
                view.UpdatePosition(newPosition);
            }
        }
    }
}