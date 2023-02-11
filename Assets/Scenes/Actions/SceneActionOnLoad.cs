using Leopotam.EcsLite;

namespace PFY.Scenes.Actions
{
    public abstract class SceneActionOnLoad
    {
        public abstract void OnAction(EcsWorld world);
    }
}