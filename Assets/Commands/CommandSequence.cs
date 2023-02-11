using System.Collections.Generic;
using Leopotam.EcsLite;

namespace PFY.Commands
{
    public sealed class CommandSequence : Command
    {
        private List<Command> _commands;

        public static Command Create(List<Command> commands)
        {
            return new CommandSequence(commands);
        }

        private CommandSequence(List<Command> commands)
        {
            _commands = commands;
        }
        
        public override void Apply(EcsWorld world)
        {
            while (_commands.Count > 0)
            {
                var command = _commands[0];
                command.Apply(world);
                _commands.Remove(command);
            }

            _commands = null;
        }
    }
}