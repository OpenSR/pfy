using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.View;
using PFY.Play.Model.Layout.Components;

namespace PFY.Play.Model.Layouts.Systems
{
    using PoolLevelSettings = EcsPoolInject
        <
            ComponentPlayLevelLayout
        >;
    
    public interface ISystemPlayLevelLayoutCreate : IEcsInitSystem { }

    public sealed class SystemPlayLevelLayoutCreate : ISystemPlayLevelLayoutCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly PoolLevelSettings _poolLevelSettings = default;

        private LevelLayout _levelLayout;

        public static ISystemPlayLevelLayoutCreate Create(LevelLayout levelLayout)
        {
            return new SystemPlayLevelLayoutCreate(levelLayout);
        }
        
        private SystemPlayLevelLayoutCreate(LevelLayout levelLayout)
        {
            _levelLayout = levelLayout;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _poolLevelSettings.Value.Add(_world.Value.NewEntity()).LevelLayout = _levelLayout;
            _levelLayout = null;
        }
    }
}