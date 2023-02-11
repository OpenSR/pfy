using System.Collections.Generic;
using PFY.Level.Ground.View;
using PFY.Level.Spawn.View;
using PFY.Level.UI.View;
using PFY.Level.Waypoint.View;
using PFY.Share;
using UnityEngine;

namespace PFY.Level.View
{
    public sealed class LevelLayout : Layout
    {
        public GroundLayout Ground => ground;
        public IReadOnlyList<Vector3> Spawns => spawns.Spawns;
        public IReadOnlyList<Vector3> Waypoints => waypoints.Waypoints;
        public LevelUiLayout Ui => ui;

        [SerializeField]
        private GroundLayout ground;
        [SerializeField]
        private SpawnsLayout spawns;
        [SerializeField]
        private WaypointsLayout waypoints;
        [SerializeField]
        private LevelUiLayout ui;
    }
}