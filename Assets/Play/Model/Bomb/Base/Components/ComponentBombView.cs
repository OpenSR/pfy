using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Types;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombView : IEcsAutoReset<ComponentBombView>
    {
        public IBombView Value;
        
        public void AutoReset(ref ComponentBombView c)
        {
            c.Value?.Destroy();
            c.Value = null;
        }
    }
}