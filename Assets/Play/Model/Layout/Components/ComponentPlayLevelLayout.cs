using Leopotam.EcsLite;
using PFY.Level;
using PFY.Level.View;

namespace PFY.Play.Model.Layout.Components
{
    public struct ComponentPlayLevelLayout : IEcsAutoReset<ComponentPlayLevelLayout>
    {
        public LevelLayout LevelLayout;
        
        public void AutoReset(ref ComponentPlayLevelLayout c)
        {
            c.LevelLayout = null;
        }
    }
}