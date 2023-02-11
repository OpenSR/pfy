using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Game.Settings.Scripts.Components;
using PFY.Utils;

namespace PFY.Game.Settings.Scripts.Systems
{
    using FilterGameSettings = EcsFilterInject
        <
            Inc
            <
                ComponentGameSettings
            >
        >;
    
    using PoolGameSettings = EcsPoolInject
        <
            ComponentGameSettings
        >;
    
    public interface ISystemGameSettingsCreate : IEcsInitSystem { }

    public sealed class SystemGameSettingsCreate : ISystemGameSettingsCreate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterGameSettings _filterGameSettings = default;
        private readonly PoolGameSettings _poolGameSettings = default;

        private GameSettings _gameSettings;

        public static ISystemGameSettingsCreate Create(GameSettings gameSettings)
        {
            return new SystemGameSettingsCreate(gameSettings);
        }
        
        private SystemGameSettingsCreate(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            _filterGameSettings.Value.DeleteAllEntitiesFromWorld();
            _poolGameSettings.Value.Add(_world.Value.NewEntity()).GameSettings = _gameSettings;
            _gameSettings = null;
        }
    }
}