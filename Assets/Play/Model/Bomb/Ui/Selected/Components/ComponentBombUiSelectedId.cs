using Leopotam.EcsLite;

namespace PFY.Play.Model.Bomb.Ui.Selected.Components
{
    public struct ComponentBombUiSelectedId : IEcsAutoReset<ComponentBombUiSelectedId>
    {
        public int Id;

        public void AutoReset(ref ComponentBombUiSelectedId c)
        {
            c.Id = -1;
        }
    }
}