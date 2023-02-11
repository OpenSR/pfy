using Leopotam.EcsLite;

namespace PFY.Loader.Components
{
    public struct ComponentLoaderUpdateProgress : IEcsAutoReset<ComponentLoaderUpdateProgress>
    {
        public int Progress;
        
        public void AutoReset(ref ComponentLoaderUpdateProgress c)
        {
            c.Progress = 0;
        }
    }
}