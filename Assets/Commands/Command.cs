using Leopotam.EcsLite;

namespace PFY.Commands
{
    public abstract class Command
    {
        public abstract void Apply(EcsWorld world);
    }
}