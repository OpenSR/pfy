using Leopotam.EcsLite;
using PFY.Commands.Components;
using PFY.Loader.Commands;
using PFY.Scenes.Actions;
using PFY.Scenes.Commands;
using PFY.Scenes.Components;
using PFY.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PFY.Scenes.Tasks
{
    public sealed class TaskSceneLoading : Task
    {
        public override bool IsFinish => _isFinish;

        private bool _isFinish;
        private AssetReference _sceneReference;
        private string _loaderHeader;
        private SceneActionOnLoad _actionOnLoad;
        private SceneActionOnUnload _actionOnUnload;
        private AsyncOperationHandle<SceneInstance> _sceneLoadingAsyncOperationHandle;

        public static Task Create(AssetReference sceneReference, string loaderHeader, SceneActionOnLoad actionOnLoad, SceneActionOnUnload actionOnUnload)
        {
            return new TaskSceneLoading(sceneReference, loaderHeader, actionOnLoad, actionOnUnload);
        }

        private TaskSceneLoading(AssetReference sceneReference, string loaderHeader, SceneActionOnLoad actionOnLoad, SceneActionOnUnload actionOnUnload)
        {
            _sceneReference = sceneReference;
            _loaderHeader = loaderHeader;
            _actionOnLoad = actionOnLoad;
            _actionOnUnload = actionOnUnload;
        }
        
        public override void Run(EcsWorld world)
        {
            _isFinish = false;
            
            var poolCommandsService = world.GetPool<ComponentCommandsService>();
            var filterCommandsService = world.Filter<ComponentCommandsService>().End();
            
            foreach (var commandsServiceEntityId in filterCommandsService)
            {
                var commandsService = poolCommandsService.Get(commandsServiceEntityId).CommandsService;
                commandsService.Enqueue(CommandLoaderUpdateHeader.Create(_loaderHeader));
                commandsService.Enqueue(CommandLoaderUpdateProgress.Create(0));
            }
            
            var poolSceneCommandOnUnload = world.GetPool<ComponentSceneActionOnUnload>();
            var filterSceneCurrent = world.Filter<ComponentSceneCurrentTag>().End();
            
            foreach (var sceneCurrentEntityId in filterSceneCurrent)
            {
                if (poolSceneCommandOnUnload.Has(sceneCurrentEntityId))
                {
                    poolSceneCommandOnUnload.Get(sceneCurrentEntityId).ActionOnUnload.OnAction(world);
                }
                world.DelEntity(sceneCurrentEntityId);
            }
            
            _sceneLoadingAsyncOperationHandle = _sceneReference.LoadSceneAsync();
        }

        public override void Update(EcsWorld world)
        {
            var poolCommandsService = world.GetPool<ComponentCommandsService>();
            var filterCommandsService = world.Filter<ComponentCommandsService>().End();
            
            if (!_sceneLoadingAsyncOperationHandle.IsDone)
            {
                foreach (var commandsServiceEntityId in filterCommandsService)
                {
                    var commandsService = poolCommandsService.Get(commandsServiceEntityId).CommandsService;
                    commandsService.Enqueue(
                        CommandLoaderUpdateProgress.Create((int)(_sceneLoadingAsyncOperationHandle.PercentComplete * 100)));
                }    
                return;
            }
            
            
            foreach (var commandsServiceEntityId in filterCommandsService)
            {
                var commandsService = poolCommandsService.Get(commandsServiceEntityId).CommandsService;
                commandsService.Enqueue(CommandSceneNewApply.Create(_actionOnLoad, _actionOnUnload));
            }
            
            _isFinish = true;
        }

        public override void Destroy()
        {
            _sceneReference = null;
            _loaderHeader = null;
            _actionOnLoad = null;
            _actionOnUnload = null;
        }
    }
}