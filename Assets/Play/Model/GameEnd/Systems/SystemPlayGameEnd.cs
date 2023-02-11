using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Game.Settings.Scripts.Components;
using PFY.Meta.Tasks;
using PFY.Play.Model.GameEnd.Components;
using PFY.Tasks.Components;
using PFY.Utils;

namespace PFY.Play.Model.GameEnd.Systems
{
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
    
    using FilterPlayGameEnd = EcsFilterInject
        <
            Inc
            <
                ComponentPlayGameEndTag
            >
        >;
    
    public interface ISystemPlayGameEnd : IEcsRunSystem { }

    public sealed class SystemPlayGameEnd : ISystemPlayGameEnd
    {
        private readonly FilterGameSettings _filterGameSettings = default;
        private readonly FilterTasksService _filterTasksService = default;
        private readonly FilterPlayGameEnd _filterPlayGameEnd = default;
        
        public static ISystemPlayGameEnd Create()
        {
            return new SystemPlayGameEnd();
        }
        
        private SystemPlayGameEnd() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if (_filterPlayGameEnd.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            
            _filterPlayGameEnd.Value.DeleteAllEntitiesFromWorld();
            var gameSettings = _filterGameSettings.Value.GetSingleComponent<ComponentGameSettings>();
            var tasksService = _filterTasksService.Value.GetSingleComponent<ComponentTasksService>();
            tasksService.TasksService.Enqueue(TaskMetaLoad.Create(gameSettings.GameSettings));
        }
    }
}