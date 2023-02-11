using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Level.Bombs.Bomb.Types;
using PFY.Level.Bombs.Settings.Scripts;
using PFY.Play.Model.Bomb.Types.Creators;

namespace PFY.Play.Model.Bomb.Types.Systems
{
    public interface ISystemBombTypeCreate : IEcsInitSystem { }

    public sealed class SystemBombTypeCreate : ISystemBombTypeCreate
    {
        private readonly EcsWorldInject _world = default;
        
        private BombsSettings _bombsSettings;
        private IFabricBombType _fabricBombType;

        public static ISystemBombTypeCreate Create(IFabricBombType fabricBombType, BombsSettings bombsSettings)
        {
            return new SystemBombTypeCreate(fabricBombType, bombsSettings);
        }
        
        private SystemBombTypeCreate(IFabricBombType fabricBombType, BombsSettings bombsSettings)
        {
            _fabricBombType = fabricBombType;
            _bombsSettings = bombsSettings;
        }
        
        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var createdBombType = new HashSet<BombTypes>();

            foreach (var bombSettings in _bombsSettings.bombSettingsList)
            {
                if (createdBombType.Contains(bombSettings.type))
                {
                    continue;
                }

                _fabricBombType.ApplyCreatorBombType(_world.Value, bombSettings);
                createdBombType.Add(bombSettings.type);
            }

            _fabricBombType = null;
            _bombsSettings = null;
        }
    }
}