using Leopotam.EcsLite;
using PFY.Commands.Service;

namespace PFY.Commands.Components
{
    public struct ComponentCommandsService : IEcsAutoReset<ComponentCommandsService>
    {
        public ICommandsService CommandsService;

        public void AutoReset(ref ComponentCommandsService c)
        {
            c.CommandsService?.Destroy();
            c.CommandsService = null;
        }
    }
}