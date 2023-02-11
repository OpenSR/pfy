using System.Collections.Generic;
using PFY.Level.Settings;
using PFY.Loader.Tasks;
using PFY.Play.Actions;
using PFY.Scenes.Tasks;
using PFY.Tasks;

namespace PFY.Play.Tasks
{
    public static class TaskPlayLoad
    {
        public static Task Create(LevelSettings levelSettings)
        {
            return TaskSequence.Create(new List<Task>
            {
                TaskLoaderShow.Create($"Load {levelSettings.loaderHeader}"),
                TaskSceneLoading.Create
                (
                    levelSettings.sceneLink, 
                    $"Load {levelSettings.loaderHeader}",
                    PlayOnLoadAction.Create(levelSettings),
                    PlayOnUnloadAction.Create()
                ),
                TaskLoaderHide.Create()
            });
        }
    }
}