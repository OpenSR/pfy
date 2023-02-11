using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Game.Model.SubModel.Components;
using PFY.Utils;

namespace PFY.Game.Model.SubModel.Systems
{
    using FilterSubModel = EcsFilterInject
        <
            Inc
            <
                ComponentCurrentSubModel
            >
        >;
    
    public interface ISystemUpdateSubModel : IEcsRunSystem { }

    public sealed class SystemUpdateSubModel : ISystemUpdateSubModel
    {
        private readonly FilterSubModel _filterSubModel = default;

        public static ISystemUpdateSubModel Create()
        {
            return new SystemUpdateSubModel();
        }
        
        private SystemUpdateSubModel() { }
        
        public void Run(IEcsSystems systems)
        {
            if (_filterSubModel.Value.GetEntitiesCount() <= 0)
            {
                return;
            }

            var subModel = _filterSubModel.Value.GetSingleComponent<ComponentCurrentSubModel>();
            subModel.SubModel.Update();
        }
    }
}