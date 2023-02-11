using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Loader.Components;

namespace PFY.Loader.Commands
{
    public sealed class CommandLoaderShow : Command
    {
        public static Command Create()
        {
            return new CommandLoaderShow();
        }
        
        private CommandLoaderShow() { }
        
        public override void Apply(EcsWorld world)
        {
            var entityId = world.NewEntity();
            world.GetPool<ComponentLoaderShowTag>().Add(entityId);
        }
    }
}