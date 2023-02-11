namespace PFY.Loader.View
{
    public sealed class LoaderView : ILoaderView
    {
        private LoaderLayout _loaderLayout;
        
        public static ILoaderView Create(LoaderLayout loaderLayout)
        {
            return new LoaderView(loaderLayout);
        }
        
        private LoaderView(LoaderLayout loaderLayout)
        {
            _loaderLayout = loaderLayout;
        }
        
        void ILoaderView.Show()
        {
            _loaderLayout.Show();
        }

        void ILoaderView.Hide()
        {
            _loaderLayout.Hide();
        }

        void ILoaderView.UpdateHeader(string str)
        {
            _loaderLayout.Header = str;
        }

        void ILoaderView.UpdateProgress(int progress)
        {
            _loaderLayout.Progress = progress;
        }

        void ILoaderView.Destroy()
        {
            _loaderLayout = null;
        }
    }
}