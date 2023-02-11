using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PFY.Level.Bombs.Bomb.Settings.Scripts.Specific.Cluster.AttackUnit
{
    [CreateAssetMenu(fileName = "ClusterBombAttackUnitSettings", menuName = "Settings/Levels/Level/Bomb/Specific/ClusterBomb/ClusterBombAttackUnitSettings", order = 1)]
    public sealed class ClusterBombAttackUnitSettings : ScriptableObject
    {
        [SerializeField]
        public AssetReference prefabReference;
        [SerializeField]
        public float attackUnitDamageRadius;
        [SerializeField]
        public List<ClusterBombAttackUnitTargetDeltaPositionSettings> attackUnitTargetDeltaPosition;
    }
}