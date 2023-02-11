using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;
using PFY.Play.Model.Bomb.Cluster.Components;

namespace PFY.Play.Model.Bomb.Cluster.Systems
{
    using FilterBombClusterActive = EcsFilterInject
        <
            Inc
            <
                ComponentBombClusterTag,
                ComponentBombIsActiveTag, 
                ComponentBombClusterFallTimeToSeparationMsec, 
                ComponentBombFallTimeCurrentMsec
            >
        >;

    using PoolBombIsActive = EcsPoolInject
        <
            ComponentBombIsActiveTag
        >;
    
    using PoolBombClusterFallTimeToSeparationMsec = EcsPoolInject
        <
            ComponentBombClusterFallTimeToSeparationMsec
        >;
    
    using PoolBombFallTimeCurrentMsec = EcsPoolInject
        <
            ComponentBombFallTimeCurrentMsec
        >;
    
    using PoolBombDetonation = EcsPoolInject
        <
            ComponentBombDetonationTag
        >;
    
    using PoolBombClusterSeparating = EcsPoolInject
        <
            ComponentBombClusterSeparatingTag
        >;

    public interface ISystemBombClusterCheckSeparate : IEcsRunSystem { }
    
    public class SystemBombClusterCheckSeparate : ISystemBombClusterCheckSeparate
    {
        private readonly FilterBombClusterActive _filterBombClusterActive = default;
        private readonly PoolBombIsActive _poolBombIsActive = default;
        private readonly PoolBombClusterFallTimeToSeparationMsec _poolBombClusterFallTimeToSeparationMsec = default;
        private readonly PoolBombFallTimeCurrentMsec _poolBombFallTimeCurrentMsec = default;
        private readonly PoolBombDetonation _poolBombDetonation = default;
        private readonly PoolBombClusterSeparating _poolBombClusterSeparating = default;
        
        public static ISystemBombClusterCheckSeparate Create()
        {
            return new SystemBombClusterCheckSeparate();
        }
        
        private SystemBombClusterCheckSeparate() { }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var bombClusterEntityId in _filterBombClusterActive.Value)
            {
                var fallTimeToSeparationMsec = _poolBombClusterFallTimeToSeparationMsec.Value.Get(bombClusterEntityId).Value;
                var fallTimeCurrentMsec = _poolBombFallTimeCurrentMsec.Value.Get(bombClusterEntityId).Value;

                if (fallTimeCurrentMsec >= fallTimeToSeparationMsec)
                {
                    _poolBombIsActive.Value.Del(bombClusterEntityId);
                    _poolBombDetonation.Value.Add(bombClusterEntityId);
                    _poolBombClusterSeparating.Value.Add(bombClusterEntityId);
                }
            }
        }
    }
}