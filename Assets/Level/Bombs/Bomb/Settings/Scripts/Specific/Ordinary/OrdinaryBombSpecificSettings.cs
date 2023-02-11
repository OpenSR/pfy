using UnityEngine;

namespace PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Ordinary
{
    [CreateAssetMenu(fileName = "OrdinaryBombSpecificSettings", menuName = "Settings/Levels/Level/Bomb/Specific/OrdinaryBombSpecificSettings", order = 1)]
    public sealed class OrdinaryBombSpecificSettings : BombSpecificSettings
    {
        [SerializeField]
        public int fallTimeMsec;
        [SerializeField]
        public float damageRadius;
    }
}