using System.Collections.Generic;
using UnityEngine;

namespace PFY.Level.Enemy.Settings.Scripts
{
    [CreateAssetMenu(fileName = "LevelEnemiesSettings", menuName = "Settings/Levels/Level/Enemy/LevelEnemiesSettings", order = 1)]
    public class LevelEnemiesSettings : ScriptableObject
    {
        [SerializeField]
        public List<LevelEnemySettings> enemySettingsList;
    }
}