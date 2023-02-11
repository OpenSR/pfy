using PFY.Game.Settings.Scripts;
using PFY.Play.Tasks;
using PFY.Tasks.Service;
using UnityEngine;

namespace PFY.Meta.View
{
    public sealed class MetaView : IMetaView
    {
        private MetaLayout _layout;
        private ITasksService _tasksService;
        private GameSettings _gameSettings;
        
        public static IMetaView Create(MetaLayout layout, ITasksService tasksService, GameSettings gameSettings)
        {
            return new MetaView(layout, tasksService, gameSettings);
        }
        
        private MetaView(MetaLayout layout, ITasksService tasksService, GameSettings gameSettings)
        {
            _layout = layout;
            _tasksService = tasksService;
            _gameSettings = gameSettings;
            
            _layout.EventOnPlayButtonClick += LayoutOnEventOnPlayButtonClick;
        }

        void IMetaView.Destroy()
        {
            _layout.EventOnPlayButtonClick -= LayoutOnEventOnPlayButtonClick;
            _layout = null;
            _tasksService = null;
            _gameSettings = null;
        }
        
        private void LayoutOnEventOnPlayButtonClick()
        {
            var levelSettingsCount = _gameSettings.levelsSettings.settingsList.Count;
            var levelSettings = _gameSettings.levelsSettings.settingsList[Random.Range(0, levelSettingsCount)];
            _tasksService.Enqueue(TaskPlayLoad.Create(levelSettings));
        }
    }
}