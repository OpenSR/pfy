using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Game.Settings.Scripts.Components;
using PFY.Game.Start.Components;
using PFY.Game.Start.Tasks;
using PFY.Tasks.Components;
using PFY.Utils;

namespace PFY.Game.Start.Systems
{
    using FilterGameStart = EcsFilterInject
        <
            Inc
            <
                ComponentGameStartTag
            >
        >;
    
    using FilterGameSettings = EcsFilterInject
        <
            Inc
            <
                ComponentGameSettings
            >
        >;
    
    using FilterTasksService = EcsFilterInject
        <
            Inc
            <
                ComponentTasksService
            >
        >;
    
    using PoolGameStart = EcsPoolInject<ComponentGameStartTag>;
    
    public interface ISystemGameStart : IEcsRunSystem { }

    public sealed class SystemGameStart : ISystemGameStart
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterGameStart _filterGameStart = default;
        private readonly FilterGameSettings _filterGameSettings = default;
        private readonly FilterTasksService _filterTasksService = default;
        private readonly PoolGameStart _poolGameStart = default;

        public static ISystemGameStart Create()
        {
            return new SystemGameStart();
        }
        
        private SystemGameStart() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterGameStart.Value.GetEntitiesCount() > 0)
            {
                return;
            }

            var tasksService = _filterTasksService.Value.GetSingleComponent<ComponentTasksService>();
            var gameSettings = _filterGameSettings.Value.GetSingleComponent<ComponentGameSettings>();
            
            _poolGameStart.Value.Add(_world.Value.NewEntity());
            tasksService.TasksService.Enqueue(TaskGameStart.Create(gameSettings.GameSettings));
        }
    }
}