using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Enemy.Components;
using PFY.Play.Model.GameEnd.Components;

namespace PFY.Play.Model.GameEnd.Systems
{
    using FilterEnemy = EcsFilterInject
        <
            Inc
            <
                ComponentPlayEnemyTag
            >
        >;
    
    using PoolPlayGameEnd = EcsPoolInject
        <
            ComponentPlayGameEndTag
        >;

    public interface ISystemPlayGameEndAllEnemyDie : IEcsRunSystem { }

    public sealed class SystemPlayGameEndAllEnemyDie : ISystemPlayGameEndAllEnemyDie
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterEnemy _filterEnemy = default;
        private readonly PoolPlayGameEnd _poolPlayGameEnd = default;
        
        public static ISystemPlayGameEndAllEnemyDie Create()
        {
            return new SystemPlayGameEndAllEnemyDie();
        }

        private SystemPlayGameEndAllEnemyDie() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterEnemy.Value.GetEntitiesCount() > 0)
            {
                return;
            }

            _poolPlayGameEnd.Value.Add(_world.Value.NewEntity());
        }
    }
}