using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Commands.Components;
using PFY.Utils;

namespace PFY.Commands.Systems
{
    using FilterCommandsService = EcsFilterInject
        <
            Inc
            <
                ComponentCommandsService
            >
        >;
    
    public interface ISystemCommandsApply : IEcsRunSystem { }

    public sealed class SystemCommandsApply : ISystemCommandsApply
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterCommandsService _filterCommandsService = default;

        public static ISystemCommandsApply Create()
        {
            return new SystemCommandsApply();
        }

        private SystemCommandsApply() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var commandsService = _filterCommandsService.Value.GetSingleComponent<ComponentCommandsService>()
                .CommandsService;
            
            if (!commandsService.TryDequeueQueue(out var commands))
            {
                return;
            }
            
            while (commands.TryDequeue(out var command))
            {
                command.Apply(_world.Value);
            }
        }
    }
}