using Leopotam.EcsLite;
using PFY.Level.Bombs.BombSelector.UI;
using PFY.Level.Bombs.BombSelector.UI.View;

namespace PFY.Play.Model.Bomb.Selector.Components
{
    public struct ComponentBombSelectorUiView : IEcsAutoReset<ComponentBombSelectorUiView>
    {
        public IBombSelectorUiView BombSelectorUiView;
        
        public void AutoReset(ref ComponentBombSelectorUiView c)
        {
            c.BombSelectorUiView?.Destroy();
            c.BombSelectorUiView = null;
        }
    }
}