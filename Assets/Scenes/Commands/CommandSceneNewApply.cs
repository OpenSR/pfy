using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Scenes.Actions;
using PFY.Scenes.Components;

namespace PFY.Scenes.Commands
{
    public sealed class CommandSceneNewApply : Command
    {
        private SceneActionOnLoad _actionOnLoad;
        private SceneActionOnUnload _actionOnUnload;
        
        public static Command Create(SceneActionOnLoad actionOnLoad, SceneActionOnUnload actionOnUnload)
        {
            return new CommandSceneNewApply(actionOnLoad, actionOnUnload);
        }

        private CommandSceneNewApply(SceneActionOnLoad actionOnLoad, SceneActionOnUnload actionOnUnload)
        {
            _actionOnLoad = actionOnLoad;
            _actionOnUnload = actionOnUnload;
        }
        
        public override void Apply(EcsWorld world)
        {
            var newSceneEntityId = world.NewEntity();
            world.GetPool<ComponentSceneNewApplyTag>().Add(newSceneEntityId);
            world.GetPool<ComponentSceneActionOnLoad>().Add(newSceneEntityId).ActionOnLoad = _actionOnLoad;
            world.GetPool<ComponentSceneActionOnUnload>().Add(newSceneEntityId).ActionOnUnload = _actionOnUnload;

            _actionOnLoad = null;
            _actionOnUnload = null;
        }
    }
}