using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;
using PFY.Play.Model.Bomb.Types.Components;

namespace PFY.Play.Model.Bomb.Types.Creators
{
    public sealed class CreatorBombTypeCluster : CreatorBombType
    {
        public override BombTypes BombType => BombTypes.Cluster;
        
        public static CreatorBombType Create()
        {
            return new CreatorBombTypeCluster();
        }
        
        private CreatorBombTypeCluster() { }

        protected override void OnSpecificCreate(EcsWorld world, BombSettings settings, int entityId)
        {
            world.GetPool<ComponentBombTypeClusterTag>().Add(entityId);
        }
    }
}