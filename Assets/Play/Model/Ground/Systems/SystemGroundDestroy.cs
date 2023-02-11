using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Ground.Components;
using PFY.Utils;

namespace PFY.Play.Model.Ground.Systems
{
    using FilterGround = EcsFilterInject
        <
            Inc
            <
                ComponentGroundTag
            >
        >;
    
    public interface ISystemGroundDestroy : IEcsDestroySystem { }

    public sealed class SystemGroundDestroy : ISystemGroundDestroy
    {
        private readonly FilterGround _filterGround = default;

        public static ISystemGroundDestroy Create()
        {
            return new SystemGroundDestroy();
        }
        
        private SystemGroundDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterGround.Value.DeleteAllEntitiesFromWorld();
        }
    }
}