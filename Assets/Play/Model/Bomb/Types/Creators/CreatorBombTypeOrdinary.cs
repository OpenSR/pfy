using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;
using PFY.Play.Model.Bomb.Types.Components;

namespace PFY.Play.Model.Bomb.Types.Creators
{
    public sealed class CreatorBombTypeOrdinary : CreatorBombType
    {
        public override BombTypes BombType => BombTypes.Ordinary;
        
        public static CreatorBombType Create()
        {
            return new CreatorBombTypeOrdinary();
        }
        
        private CreatorBombTypeOrdinary() { }
        
        protected override void OnSpecificCreate(EcsWorld world, BombSettings settings, int entityId)
        {
            world.GetPool<ComponentBombTypeOrdinaryTag>().Add(entityId);
        }
    }
}