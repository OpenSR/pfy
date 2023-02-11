namespace PFY.Loader.View
{
    public interface ILoaderView
    {
        void Show();
        void Hide();
        void UpdateHeader(string str);
        void UpdateProgress(int progress);
        void Destroy();
    }
}