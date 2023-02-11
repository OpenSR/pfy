using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PFY.Level.Enemy.Settings.Scripts
{
    [CreateAssetMenu(fileName = "LevelEnemySettings", menuName = "Settings/Levels/Level/Enemy/LevelEnemySettings", order = 1)]
    public class LevelEnemySettings : ScriptableObject
    {
        [SerializeField]
        public AssetReference enemyPrefabReference;
        [SerializeField]
        public float damagePerceptionRadius;
        [SerializeField]
        public float moveSpeed;
    }
}