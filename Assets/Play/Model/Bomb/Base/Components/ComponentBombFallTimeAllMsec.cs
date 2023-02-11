using Leopotam.EcsLite;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombFallTimeAllMsec : IEcsAutoReset<ComponentBombFallTimeAllMsec>
    {
        public int Value;
        
        public void AutoReset(ref ComponentBombFallTimeAllMsec c)
        {
            c.Value = 1;
        }
    }
}