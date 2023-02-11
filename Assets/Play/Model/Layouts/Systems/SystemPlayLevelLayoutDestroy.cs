using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PFY.Play.Model.Layout.Components;
using PFY.Utils;

namespace PFY.Play.Model.Layouts.Systems
{
    using FilterLevelSettings = EcsFilterInject
        <
            Inc
            <
                ComponentPlayLevelLayout
            >
        >;
    
    public interface ISystemPlayLevelLayoutDestroy  : IEcsDestroySystem { }

    public sealed class SystemPlayLevelLayoutDestroy : ISystemPlayLevelLayoutDestroy
    {
        private readonly FilterLevelSettings _filterLevelSettings = default;

        public static ISystemPlayLevelLayoutDestroy Create()
        {
            return new SystemPlayLevelLayoutDestroy();
        }
        
        private SystemPlayLevelLayoutDestroy() { }
        
        void IEcsDestroySystem.Destroy(IEcsSystems systems)
        {
            _filterLevelSettings.Value.DeleteAllEntitiesFromWorld();
        }
    }
}