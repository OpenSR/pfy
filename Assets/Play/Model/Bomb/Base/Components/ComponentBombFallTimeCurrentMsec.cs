using Leopotam.EcsLite;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombFallTimeCurrentMsec : IEcsAutoReset<ComponentBombFallTimeCurrentMsec>
    {
        public int Value;
        
        public void AutoReset(ref ComponentBombFallTimeCurrentMsec c)
        {
            c.Value = 0;
        }
    }
}