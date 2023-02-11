using PFY.Level.Bombs.Bomb.Settings.Scripts.Specific;
using PFY.Level.Bombs.Bomb.Types;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PFY.Level.Bombs.Bomb.Settings.Scripts
{
    [CreateAssetMenu(fileName = "BombSettings", menuName = "Settings/Levels/Level/Bomb/BombSettings", order = 1)]
    public class BombSettings : ScriptableObject
    {
        [SerializeField]
        public AssetReference iconReference;
        [SerializeField]
        public AssetReference prefabUiReference;
        [SerializeField]
        public AssetReference prefabReference;
        [SerializeField]
        public BombTypes type;
        [SerializeField]
        public BombSpecificSettings specificSettings;
    }
}