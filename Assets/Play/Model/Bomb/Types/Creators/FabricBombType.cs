using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;

namespace PFY.Play.Model.Bomb.Types.Creators
{
    public sealed class FabricBombType : IFabricBombType
    {
        private readonly Dictionary<BombTypes, CreatorBombType> _creators;

        public static IFabricBombType Create()
        {
            return new FabricBombType();
        }

        private FabricBombType()
        {
            _creators = new Dictionary<BombTypes, CreatorBombType>();
        }

        public void RegistrationCreatorBombType(CreatorBombType creator)
        {
            if (_creators.ContainsKey(creator.BombType))
            {
                throw new Exception($"CreatorBombType for type {creator.BombType} already added.");
            }
            
            _creators.Add(creator.BombType, creator);
        }

        public void CheckTypes()
        {
            foreach (var bombType in (BombTypes[]) Enum.GetValues(typeof(BombTypes)))
            {
                if (!_creators.ContainsKey(bombType))
                {
                    throw new Exception($"CreatorBombType for type {bombType} don't added.");
                }
            }
        }

        public void ApplyCreatorBombType(EcsWorld world, BombSettings settings)
        {
            if (!_creators.TryGetValue(settings.type, out var creatorBombType))
            {
                throw new Exception($"CreatorBombType for type {settings.type} not found.");
            }
            
            creatorBombType.OnCreate(world, settings);
        }
    }
}