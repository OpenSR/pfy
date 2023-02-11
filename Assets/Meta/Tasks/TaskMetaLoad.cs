using System.Collections.Generic;
using PFY.Game.Settings.Scripts;
using PFY.Loader.Tasks;
using PFY.Meta.Actions;
using PFY.Scenes.Tasks;
using PFY.Tasks;

namespace PFY.Meta.Tasks
{
    public static class TaskMetaLoad
    {
        public static Task Create(GameSettings gameSettings)
        {
            return TaskSequence.Create(new List<Task>
            {
                TaskLoaderShow.Create("Load Meta"),
                TaskSceneLoading.Create
                (
                    gameSettings.metaSceneLink, 
                    "Load Meta", 
                    MetaOnLoadAction.Create(),
                    MetaOnUnloadAction.Create()
                ),
                TaskLoaderHide.Create()
            });
        }
    }
}