using System;
using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;
using PFY.Play.Model.Bomb.Types.Components;

namespace PFY.Play.Model.Bomb.Types.Creators
{
    public abstract class CreatorBombType
    {
        public abstract BombTypes BombType { get; }

        public void OnCreate(EcsWorld world, BombSettings settings)
        {
            if (BombType != settings.type)
            {
                throw new Exception("CreatorBombType wrong bomb type in settings.");
            }
            
            var entityId = world.NewEntity();
            world.GetPool<ComponentBombTypeTag>().Add(entityId);
            world.GetPool<ComponentBombType>().Add(entityId).BombType = BombType;
            world.GetPool<ComponentBombTypeSettings>().Add(entityId).BombSettings = settings;
            OnSpecificCreate(world, settings, entityId);
        }

        protected abstract void OnSpecificCreate(EcsWorld world, BombSettings settings, int entityId);
    }
}