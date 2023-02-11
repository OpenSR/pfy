using PFY.Level.Bombs.Settings.Scripts;
using PFY.Level.Enemy.Settings.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PFY.Level.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/Levels/LevelSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField]
        public string loaderHeader = string.Empty;
        [SerializeField]
        public AssetReference sceneLink;
        [SerializeField]
        public int enemyToCreateCount;
        [SerializeField]
        public LevelEnemiesSettings enemiesSettings;
        [SerializeField]
        public BombsSettings bombsSettings;
    }
}