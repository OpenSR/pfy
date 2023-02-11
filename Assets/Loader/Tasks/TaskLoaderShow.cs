using Leopotam.EcsLite;
using PFY.Commands.Components;
using PFY.Loader.Commands;
using PFY.Tasks;

namespace PFY.Loader.Tasks
{
    public sealed class TaskLoaderShow : Task
    {
        public override bool IsFinish => _isFinish;

        private bool _isFinish;
        private string _header;

        public static Task Create(string header)
        {
            return new TaskLoaderShow(header);
        }

        private TaskLoaderShow(string header)
        {
            _header = header;
        }
        
        public override void Run(EcsWorld world)
        {
            var poolCommandsService = world.GetPool<ComponentCommandsService>();
            var filterCommandsService = world.Filter<ComponentCommandsService>().End();
            
            foreach (var commandsServiceEntityId in filterCommandsService)
            {
                var commandsService = poolCommandsService.Get(commandsServiceEntityId).CommandsService;
                commandsService.Enqueue(CommandLoaderShow.Create());
                commandsService.Enqueue(CommandLoaderUpdateHeader.Create(_header));
                commandsService.Enqueue(CommandLoaderUpdateProgress.Create(0));
            }

            _header = null;
            _isFinish = true;
        }

        public override void Update(EcsWorld world) { }

        public override void Destroy() { }
    }
}