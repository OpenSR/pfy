using Leopotam.EcsLite;
using PFY.Level.UI;
using PFY.Level.UI.View;

namespace PFY.Play.Model.UI.Components
{
    public struct ComponentPlayLevelUiView : IEcsAutoReset<ComponentPlayLevelUiView>
    {
        public ILevelUiView LevelUiView;
        
        public void AutoReset(ref ComponentPlayLevelUiView c)
        {
            c.LevelUiView?.Destroy();
            c.LevelUiView = null;
        }
    }
}