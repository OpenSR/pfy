namespace PFY.Level.UI.View
{
    public sealed class LevelUiView : ILevelUiView
    {
        private LevelUiLayout _layout;
        private bool _isGameEndButtonClick;

        public static ILevelUiView Create(LevelUiLayout layout)
        {
            return new LevelUiView(layout);
        }

        private LevelUiView(LevelUiLayout layout)
        {
            _layout = layout;
            _layout.EventOnGameEndButtonClick += LayoutOnEventOnGameEndButtonClick;
        }

        bool ILevelUiView.IsGameEndButtonClick()
        {
            if (!_isGameEndButtonClick)
            {
                return false;
            }
            
            _isGameEndButtonClick = false;
            return true;

        }

        void ILevelUiView.Destroy()
        {
            _layout.EventOnGameEndButtonClick -= LayoutOnEventOnGameEndButtonClick;
            _layout = null;
        }
        
        private void LayoutOnEventOnGameEndButtonClick()
        {
            _isGameEndButtonClick = true;
        }
    }
}