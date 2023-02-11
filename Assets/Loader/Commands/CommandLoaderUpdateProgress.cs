using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Loader.Components;

namespace PFY.Loader.Commands
{
    public sealed class CommandLoaderUpdateProgress : Command
    {
        private readonly int _progress;

        public static Command Create(int progress)
        {
            return new CommandLoaderUpdateProgress(progress);
        }

        private CommandLoaderUpdateProgress(int progress)
        {
            _progress = progress;
        }
        
        public override void Apply(EcsWorld world)
        {
            var entityId = world.NewEntity();
            world.GetPool<ComponentLoaderUpdateProgress>().Add(entityId).Progress = _progress;
        }
    }
}