using System.Collections.Generic;
using PFY.Game.Settings.Scripts;
using PFY.Loader.Tasks;
using PFY.Meta.Actions;
using PFY.Scenes.Tasks;
using PFY.Tasks;

namespace PFY.Game.Start.Tasks
{
    public static class TaskGameStart
    {
        public static Task Create(GameSettings gameSettings)
        {
            return TaskSequence.Create(new List<Task>
            {
                TaskLoaderShow.Create("Load Game"),
                TaskSceneLoading.Create
                (
                    gameSettings.metaSceneLink, 
                    "Load Game", 
                    MetaOnLoadAction.Create(),
                    MetaOnUnloadAction.Create()
                ),
                TaskLoaderHide.Create()
            });
        }
    }
}