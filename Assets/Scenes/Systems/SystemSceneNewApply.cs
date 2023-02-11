using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Scenes.Components;

namespace PFY.Scenes.Systems
{
    using FilterSceneNew = EcsFilterInject
        <
            Inc
            <
                ComponentSceneNewApplyTag, 
                ComponentSceneActionOnLoad,
                ComponentSceneActionOnUnload
            >
        >;
    
    using PoolSceneNewApply = EcsPoolInject
        <
            ComponentSceneNewApplyTag
        >;
    
    using PoolSceneCommandOnLoad = EcsPoolInject
        <
            ComponentSceneActionOnLoad
        >;
    
    using PoolSceneCommandOnUnload = EcsPoolInject
        <
            ComponentSceneActionOnUnload
        >;
    
    using poolSceneCurrent = EcsPoolInject
        <
            ComponentSceneCurrentTag
        >;
    
    public interface ISystemSceneNewApply : IEcsRunSystem { }

    public sealed class SystemSceneNewApply : ISystemSceneNewApply
    {
        private readonly EcsWorldInject _world;
        private readonly FilterSceneNew _filterSceneNew;
        private readonly PoolSceneNewApply _poolSceneNewApply;
        private readonly PoolSceneCommandOnLoad _poolSceneCommandOnLoad;
        private readonly PoolSceneCommandOnUnload _poolSceneCommandOnUnload;
        private readonly poolSceneCurrent _poolSceneCurrent;

        public static ISystemSceneNewApply Create()
        {
            return new SystemSceneNewApply();
        }
        
        private SystemSceneNewApply() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var sceneNewEntityId in _filterSceneNew.Value)
            {
                _poolSceneNewApply.Value.Del(sceneNewEntityId);
                _poolSceneCurrent.Value.Add(sceneNewEntityId);
                _poolSceneCommandOnLoad.Value.Get(sceneNewEntityId).ActionOnLoad.OnAction(_world.Value);
                _poolSceneCommandOnLoad.Value.Del(sceneNewEntityId);
            }
        }
    }
}