using System.Collections.Generic;

namespace PFY.Commands.Service
{
    public sealed class CommandsService : ICommandsService
    {
        private readonly Queue<Queue<Command>> _commands;

        public static ICommandsService Create()
        {
            return new CommandsService();
        }

        private CommandsService()
        {
            _commands = new Queue<Queue<Command>>();
        }

        void ICommandsService.Enqueue(Command command)
        {
            if (!_commands.TryPeek(out var commands))
            {
                commands = new Queue<Command>();
                _commands.Enqueue(commands);
            }
            
            commands.Enqueue(command);
        }

        bool ICommandsService.TryDequeueQueue(out Queue<Command> commands)
        {
            return _commands.TryDequeue(out commands);
        }

        void ICommandsService.Destroy()
        {
            _commands.Clear();
        }
    }
}