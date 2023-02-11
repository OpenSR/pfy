using System.Collections.Generic;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using UnityEngine;

namespace PFY.Level.Bombs.Settings.Scripts
{
    [CreateAssetMenu(fileName = "BombsSettings", menuName = "Settings/Levels/Level/Bomb/BombsSettings", order = 1)]
    public class BombsSettings : ScriptableObject
    {
        [SerializeField]
        public List<BombSettings> bombSettingsList;
    }
}