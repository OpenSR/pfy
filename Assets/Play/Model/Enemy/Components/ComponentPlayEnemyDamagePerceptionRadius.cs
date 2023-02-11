using Leopotam.EcsLite;

namespace PFY.Play.Model.Enemy.Components
{
    public struct ComponentPlayEnemyDamagePerceptionRadius : IEcsAutoReset<ComponentPlayEnemyDamagePerceptionRadius>
    {
        public float DamagePerceptionRadius;
        
        public void AutoReset(ref ComponentPlayEnemyDamagePerceptionRadius c)
        {
            c.DamagePerceptionRadius = 0f;
        }
    }
}