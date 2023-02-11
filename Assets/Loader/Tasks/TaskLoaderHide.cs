using Leopotam.EcsLite;
using PFY.Commands.Components;
using PFY.Loader.Commands;
using PFY.Tasks;

namespace PFY.Loader.Tasks
{
    public sealed class TaskLoaderHide : Task
    {
        public override bool IsFinish => _isFinish;

        private bool _isFinish;

        public static Task Create()
        {
            return new TaskLoaderHide();
        }
        
        private TaskLoaderHide() { }
        
        public override void Run(EcsWorld world)
        {
            var poolCommandsService = world.GetPool<ComponentCommandsService>();
            var filterCommandsService = world.Filter<ComponentCommandsService>().End();
            
            foreach (var commandsServiceEntityId in filterCommandsService)
            {
                var commandsService = poolCommandsService.Get(commandsServiceEntityId).CommandsService;
                commandsService.Enqueue(CommandLoaderHide.Create());
            }

            _isFinish = true;
        }

        public override void Update(EcsWorld world) { }

        public override void Destroy() { }
    }
}