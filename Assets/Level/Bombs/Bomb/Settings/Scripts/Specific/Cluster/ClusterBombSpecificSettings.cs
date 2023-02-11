using PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster.AttackUnit;
using UnityEngine;

namespace PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster
{
    [CreateAssetMenu(fileName = "ClusterBombSpecificSettings", menuName = "Settings/Levels/Level/Bomb/Specific/ClusterBombSpecificSettings", order = 1)]
    public sealed class ClusterBombSpecificSettings : BombSpecificSettings
    {
        [SerializeField]
        public int fallTimeMsec;
        [SerializeField]
        public int timeToSeparationMsec;
        [SerializeField]
        public ClusterBombAttackUnitSettings attackUnitSettings;
    }
}