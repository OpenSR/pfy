using Leopotam.EcsLite;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombDamageRadiusInUnit : IEcsAutoReset<ComponentBombDamageRadiusInUnit>
    {
        public float Value;
        
        public void AutoReset(ref ComponentBombDamageRadiusInUnit c)
        {
            c.Value = 0f;
        }
    }
}