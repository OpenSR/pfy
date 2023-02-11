using Leopotam.EcsLite;
using PFY.Scenes.Actions;

namespace PFY.Scenes.Components
{
    public struct ComponentSceneActionOnUnload : IEcsAutoReset<ComponentSceneActionOnUnload>
    {
        public SceneActionOnUnload ActionOnUnload;
        
        public void AutoReset(ref ComponentSceneActionOnUnload c)
        {
            c.ActionOnUnload = null;
        }
    }
}