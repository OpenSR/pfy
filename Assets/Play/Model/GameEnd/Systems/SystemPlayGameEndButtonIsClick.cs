using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.GameEnd.Components;
using PFY.Play.Model.UI.Components;
using PFY.Utils;

namespace PFY.Play.Model.GameEnd.Systems
{
    using FilterPlayLevelUiView = EcsFilterInject
        <
            Inc
            <
                ComponentPlayLevelUiView
            >
        >;
    
    using PoolPlayGameEnd = EcsPoolInject
        <
            ComponentPlayGameEndTag
        >;
    
    public interface ISystemPlayGameEndButtonIsClick : IEcsRunSystem { }

    public sealed class SystemPlayGameEndButtonIsClick : ISystemPlayGameEndButtonIsClick
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterPlayLevelUiView _filterPlayLevelUiView = default;
        private readonly PoolPlayGameEnd _poolPlayGameEnd = default;
        
        public static ISystemPlayGameEndButtonIsClick Create()
        {
            return new SystemPlayGameEndButtonIsClick();
        }
        
        private SystemPlayGameEndButtonIsClick() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterPlayLevelUiView.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            var levelUiView = _filterPlayLevelUiView.Value.GetSingleComponent<ComponentPlayLevelUiView>().LevelUiView;
            if (levelUiView.IsGameEndButtonClick())
            {
                _poolPlayGameEnd.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}