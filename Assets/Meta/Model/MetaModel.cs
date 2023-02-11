using Leopotam.EcsLite;
using PFY.Game.Model.SubModel;
using PFY.Meta.Model.View.Systems;
using PFY.Meta.View;

namespace PFY.Meta.Model
{
    public sealed class MetaModel : SubModel
    {
        public static SubModel Create(EcsWorld world, MetaLayout layout)
        {
            return new MetaModel(world, layout);
        }
        
        private MetaModel(EcsWorld world, MetaLayout layout) : base(world)
        {
            AddSystem(SystemMetaViewCreate.Create(layout));
            AddSystem(SystemMetaViewDestroy.Create());
            Inject();
        }
    }
}