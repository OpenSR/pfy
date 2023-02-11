using Leopotam.EcsLite;

namespace PFY.Tasks.Components
{
    public struct ComponentTasksCurrent : IEcsAutoReset<ComponentTasksCurrent>
    {
        public Task Task;

        public void AutoReset(ref ComponentTasksCurrent c)
        {
            c.Task?.Destroy();
            c.Task = null;
        }
    }
}