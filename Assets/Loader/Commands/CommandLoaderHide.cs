using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Loader.Components;

namespace PFY.Loader.Commands
{
    public sealed class CommandLoaderHide : Command
    {
        public static Command Create()
        {
            return new CommandLoaderHide();
        }
        
        private CommandLoaderHide() { }
        
        public override void Apply(EcsWorld world)
        {
            var entityId = world.NewEntity();
            world.GetPool<ComponentLoaderHideTag>().Add(entityId);
        }
    }
}