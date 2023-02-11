using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Loader.Components;

namespace PFY.Loader.Commands
{
    public sealed class CommandLoaderUpdateHeader : Command
    {
        private string _str;

        public static Command Create(string str)
        {
            return new CommandLoaderUpdateHeader(str);
        }
        
        private CommandLoaderUpdateHeader(string str)
        {
            _str = str;
        }
        
        public override void Apply(EcsWorld world)
        {
            var entityId = world.NewEntity();
            world.GetPool<ComponentLoaderUpdateHeader>().Add(entityId).Text = _str;
            _str = null;
        }
    }
}