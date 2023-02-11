using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Bomb.Base.Components;

namespace PFY.Play.Model.Bomb.Base.Systems
{
    using FilterBombUnActive = EcsFilterInject
        <
            Inc
            <
                ComponentBombView
            >, 
            Exc
            <
                ComponentBombIsActiveTag
            >
        >;
    
    using PoolBombIsActive = EcsPoolInject
        <
            ComponentBombIsActiveTag
        >;
    
    using PoolBombView = EcsPoolInject
        <
            ComponentBombView
        >;
    
    public interface ISystemBombCheckIsActive : IEcsRunSystem { }

    public sealed class SystemBombCheckIsActive : ISystemBombCheckIsActive
    {
        private readonly FilterBombUnActive _filterBombUnActive = default;
        private readonly PoolBombIsActive _poolBombIsActive = default;
        private readonly PoolBombView _poolBombView = default;
        
        public static ISystemBombCheckIsActive Create()
        {
            return new SystemBombCheckIsActive();
        }
        
        private SystemBombCheckIsActive() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var bombEntityId in _filterBombUnActive.Value)
            {
                if (!_poolBombView.Value.Get(bombEntityId).Value.IsActive)
                {
                    continue;
                }

                _poolBombIsActive.Value.Add(bombEntityId);
            }
        }
    }
}