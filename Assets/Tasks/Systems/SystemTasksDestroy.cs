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
    
    public interface ISystemTasksDestroy : IEcsRunSystem { }

    public sealed class SystemTasksDestroy : ISystemTasksDestroy
    {
        private readonly EcsWorldInject _world = default;
        private readonly FilterTasksCurrent _filterTasksCurrent = default;
        private readonly PoolTasksCurrent _poolTasksCurrent = default;

        public static ISystemTasksDestroy Create()
        {
            return new SystemTasksDestroy();
        }
        
        private SystemTasksDestroy() { }
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var tasksCurrentEntityId in _filterTasksCurrent.Value)
            {
                if (_poolTasksCurrent.Value.Get(tasksCurrentEntityId).Task.IsFinish)
                {
                    _world.Value.DelEntity(tasksCurrentEntityId);
                }
            }
        }
    }
}