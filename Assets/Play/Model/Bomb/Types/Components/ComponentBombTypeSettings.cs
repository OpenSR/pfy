using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;

namespace PFY.Play.Model.Bomb.Types.Components
{
    public struct ComponentBombTypeSettings : IEcsAutoReset<ComponentBombTypeSettings>
    {
        public BombSettings BombSettings;

        public void AutoReset(ref ComponentBombTypeSettings c)
        {
            c.BombSettings = null;
        }
    }
}