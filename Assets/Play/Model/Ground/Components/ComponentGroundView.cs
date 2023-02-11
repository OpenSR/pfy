using Leopotam.EcsLite;
using PFY.Level.Ground;
using PFY.Level.Ground.View;

namespace PFY.Play.Model.Ground.Components
{
    public struct ComponentGroundView : IEcsAutoReset<ComponentGroundView>
    {
        public IGroundView GroundView;
        
        public void AutoReset(ref ComponentGroundView c)
        {
            c.GroundView?.Destroy();
            c.GroundView = null;
        }
    }
}