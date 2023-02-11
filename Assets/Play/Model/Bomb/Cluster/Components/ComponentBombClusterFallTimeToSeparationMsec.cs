using Leopotam.EcsLite;

namespace PFY.Play.Model.Bomb.Cluster.Components
{
    public struct ComponentBombClusterFallTimeToSeparationMsec : IEcsAutoReset<ComponentBombClusterFallTimeToSeparationMsec>
    {
        public int Value;
        
        public void AutoReset(ref ComponentBombClusterFallTimeToSeparationMsec c)
        {
            c.Value = 1;
        }
    }
}