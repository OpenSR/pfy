using Leopotam.EcsLite;
using PFY.Loader.View;

namespace PFY.Loader.Components
{
    public struct ComponentLoaderView : IEcsAutoReset<ComponentLoaderView>
    {
        public ILoaderView LoaderView;

        public void AutoReset(ref ComponentLoaderView c)
        {
            c.LoaderView?.Destroy();
            c.LoaderView = null;
        }
    }
}