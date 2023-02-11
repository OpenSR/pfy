using PFY.Level.Settings.Levels;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PFY.Game.Settings.Scripts
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        public LevelsSettings levelsSettings;
        [SerializeField]
        public AssetReference metaSceneLink;
    }
}