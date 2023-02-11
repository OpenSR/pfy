using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Tasks.Components;

namespace PFY.Tasks.Systems
{
    using FilterTasksCurrent = EcsFilterInject
        <
            Inc
            <
                ComponentTasksCurrent
            >
        >;
    
    using PoolTasksCurrent = EcsPoolInject
        <
            ComponentTasksCurrent
        >;
    
    public interface ISystemTasksUpdate : IEcsRunSystem { }

    public sealed class SystemTasksUpdate : ISystemTasksUpdate
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterTasksCurrent _filterTasksCurrent = default;
        private readonly PoolTasksCurrent _poolTasksCurrent = default;

        public static ISystemTasksUpdate Create()
        {
            return new SystemTasksUpdate();
        }
        
        private SystemTasksUpdate() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var tasksCurrentEntityId in _filterTasksCurrent.Value)
            {
                _poolTasksCurrent.Value.Get(tasksCurrentEntityId).Task.Update(_world.Value);
            }
        }
    }
}