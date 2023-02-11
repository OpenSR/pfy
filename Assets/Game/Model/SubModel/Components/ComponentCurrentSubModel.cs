using Leopotam.EcsLite;

namespace PFY.Game.Model.SubModel.Components
{
    public struct ComponentCurrentSubModel : IEcsAutoReset<ComponentCurrentSubModel>
    {
        public SubModel SubModel;
        
        public void AutoReset(ref ComponentCurrentSubModel c)
        {
            c.SubModel?.Destroy();
            c.SubModel = null;
        }
    }
}