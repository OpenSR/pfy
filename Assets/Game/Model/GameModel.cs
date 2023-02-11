using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif
using PFY.Commands.Systems;
using PFY.Game.Model.SubModel.Systems;
using PFY.Game.Settings.Scripts;
using PFY.Game.Settings.Scripts.Systems;
using PFY.Game.Start.Systems;
using PFY.Loader;
using PFY.Loader.Systems;
using PFY.Loader.View;
using PFY.Scenes.Systems;
using PFY.Tasks.Systems;
#if UNITY_EDITOR
#endif

namespace PFY.Game.Model
{
    public sealed class GameModel : IGameModel
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        
        public static IGameModel Create(LoaderLayout loaderLayout, GameSettings gameSettings)
        {
            return new GameModel(loaderLayout, gameSettings);
        }

        private GameModel(LoaderLayout loaderLayout, GameSettings gameSettings)
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            // Create systems
            {
                _systems.Add(SystemGameSettingsCreate.Create(gameSettings));
                _systems.Add(SystemLoaderViewCreate.Create(loaderLayout));
                _systems.Add(SystemTasksServiceCreate.Create());
                _systems.Add(SystemCommandsServiceCreate.Create());
            }

            // Update systems
            {
                // Game logic
                _systems.Add(SystemGameStart.Create());
                
                // Tasks logic
                _systems.Add(SystemTasksRun.Create());
                _systems.Add(SystemTasksUpdate.Create());
                _systems.Add(SystemTasksDestroy.Create());
                
                // Commands logic
                _systems.Add(SystemCommandsApply.Create());

                // Loader logic
                _systems.Add(SystemLoaderShow.Create());
                _systems.Add(SystemLoaderHide.Create());
                _systems.Add(SystemLoaderUpdateHeader.Create());
                _systems.Add(SystemLoaderUpdateProgress.Create());
                
                // Scene managment
                _systems.Add(SystemSceneNewApply.Create());
                
                // SubModel update
                _systems.Add(SystemUpdateSubModel.Create());

                // ECS debug
#if UNITY_EDITOR
                _systems.Add(new EcsWorldDebugSystem());
#endif
            }
            
            // Destroy systems
            {
                
            }

            _systems.Inject();
            _systems.Init();
        }

        void IGameModel.Update()
        {
            _systems?.Run();
        }

        void IGameModel.Destroy()
        {
            _systems?.Destroy();
            _systems = null;
            
            _world?.Destroy();
            _world = null;           
        }
    }
}