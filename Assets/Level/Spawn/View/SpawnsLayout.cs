using System.Collections.Generic;
using PFY.Share;
using UnityEngine;

namespace PFY.Level.Spawn.View
{
    public sealed class SpawnsLayout : Layout
    {
        public IReadOnlyList<Vector3> Spawns => _spawns;

        [SerializeField]
        private List<SpawnLayout> spawnPoints;

        private List<Vector3> _spawns;

        private void Awake()
        {
            _spawns = new List<Vector3>(spawnPoints.Count);
            foreach (var spawnPoint in spawnPoints)
            {
                _spawns.Add(spawnPoint.Position);
            }
        }
    }
}