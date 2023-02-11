using Leopotam.EcsLite;
using PFY.Meta.View;

namespace PFY.Meta.Model.View.Components
{
    public struct ComponentMetaView : IEcsAutoReset<ComponentMetaView>
    {
        public IMetaView MetaView;

        public void AutoReset(ref ComponentMetaView c)
        {
            c.MetaView?.Destroy();
            c.MetaView = null;
        }
    }
}