using Leopotam.EcsLite;

namespace PFY.Loader.Components
{
    public struct ComponentLoaderUpdateHeader : IEcsAutoReset<ComponentLoaderUpdateHeader>
    {
        public string Text;

        public void AutoReset(ref ComponentLoaderUpdateHeader c)
        {
            c.Text = string.Empty;
        }
    }
}