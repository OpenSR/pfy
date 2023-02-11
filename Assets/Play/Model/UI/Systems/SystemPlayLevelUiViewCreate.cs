using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.UI.View;
using PFY.Play.Model.UI.Components;

namespace PFY.Play.Model.UI.Systems
{
    using PoolPlayLevelUiView = EcsPoolInject
        <
            ComponentPlayLevelUiView
        >;
    
    public interface ISystemPlayLevelUiViewCreate : IEcsInitSystem { }

    public sealed class SystemPlayLevelUiViewCreate : ISystemPlayLevelUiViewCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly PoolPlayLevelUiView _poolPlayLevelUiView = default;
        
        private LevelUiLayout _layout;

        public static ISystemPlayLevelUiViewCreate Create(LevelUiLayout layout)
        {
            return new SystemPlayLevelUiViewCreate(layout);
        }
        
        private SystemPlayLevelUiViewCreate(LevelUiLayout layout)
        {
            _layout = layout;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _poolPlayLevelUiView.Value.Add(_world.Value.NewEntity()).LevelUiView = LevelUiView.Create(_layout);
            _layout = null;
        }
    }
}