using Leopotam.EcsLite;
using PFY.Scenes.Actions;

namespace PFY.Scenes.Components
{
    public struct ComponentSceneActionOnLoad : IEcsAutoReset<ComponentSceneActionOnLoad>
    {
        public SceneActionOnLoad ActionOnLoad;
        
        public void AutoReset(ref ComponentSceneActionOnLoad c)
        {
            c.ActionOnLoad = null;
        }
    }
}