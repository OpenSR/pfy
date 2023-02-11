using PFY.Share;
using UnityEngine;
using UnityEngine.UI;

namespace PFY.Level.Bombs.BombSelector.UI.View
{
    public sealed class BombSelectorUiLayout : Layout
    {
        public ScrollRect ScrollRect => scrollRect;
        
        [SerializeField]
        private ScrollRect scrollRect;
    }
}