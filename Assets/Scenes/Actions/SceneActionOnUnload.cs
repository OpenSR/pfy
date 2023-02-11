using Leopotam.EcsLite;

namespace PFY.Scenes.Actions
{
    public abstract class SceneActionOnUnload
    {
        public abstract void OnAction(EcsWorld world);
    }
}