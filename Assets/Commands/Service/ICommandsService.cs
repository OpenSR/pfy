using System.Collections.Generic;

namespace PFY.Commands.Service
{
    public interface ICommandsService
    {
        void Enqueue(Command command);
        bool TryDequeueQueue(out Queue<Command> commands);
        void Destroy();
    }
}