using System;
using Leopotam.EcsLite;

namespace PFY.Utils
{
    public static class UtilsEcs
    {
        public static void DeleteAllEntitiesFromWorld(this EcsFilter filter)
        {
            var world = filter.GetWorld();
            foreach (var entityId in filter)
            {
                world.DelEntity(entityId);
            }
        }
        
        public static ref T GetSingleComponent<T>(this EcsFilter filter) where T : struct
        {
            if (filter.GetEntitiesCount() != 1)
            {
                throw new Exception("Filter is empty or more than one entity with this component.");
            }
            
            var world = filter.GetWorld();
            return ref world.GetPool<T>().Get(filter.GetRawEntities()[0]);
        }

        public static bool TryGetComponent<T>(this EcsWorld world, int entityId, ref T component) where T : struct
        {
            var pool = world.GetPool<T>();

            if (pool.Has(entityId))
            {
                component = ref pool.Get(entityId);
                return true;
            }

            return false;
        }
    }
}