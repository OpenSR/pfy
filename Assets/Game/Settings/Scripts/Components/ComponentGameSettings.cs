using Leopotam.EcsLite;

namespace PFY.Game.Settings.Scripts.Components
{
    public struct ComponentGameSettings : IEcsAutoReset<ComponentGameSettings>
    {
        public GameSettings GameSettings;
        
        public void AutoReset(ref ComponentGameSettings c)
        {
            c.GameSettings = null;
        }
    }
}