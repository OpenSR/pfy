using UnityEngine;

namespace PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster.AttackUnit
{
    [CreateAssetMenu(fileName = "ClusterBombAttackUnitTargetDeltaPositionSettings", menuName = "Settings/Levels/Level/Bomb/Specific/ClusterBomb/ClusterBombAttackUnitTargetDeltaPositionSettings", order = 1)]
    public class ClusterBombAttackUnitTargetDeltaPositionSettings : ScriptableObject
    {
        [SerializeField]
        public float dx;
        [SerializeField]
        public float dz;
    }
}