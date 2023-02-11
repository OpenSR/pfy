using System.Collections.Generic;
using UnityEngine;

namespace PFY.Level.Settings.Levels
{
    [CreateAssetMenu(fileName = "LevelsSettings", menuName = "Settings/Levels/LevelsSettings", order = 1)]
    public class LevelsSettings : ScriptableObject
    {
        [SerializeField]
        public List<LevelSettings> settingsList;
    }
}