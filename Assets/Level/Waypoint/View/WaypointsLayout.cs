using System.Collections.Generic;
using PFY.Share;
using UnityEngine;

namespace PFY.Level.Waypoint.View
{
    public sealed class WaypointsLayout : Layout
    {
        public IReadOnlyList<Vector3> Waypoints => _waypoints;

        [SerializeField]
        private List<WaypointLayout> waypoints;

        private List<Vector3> _waypoints;

        private void Awake()
        {
            _waypoints = new List<Vector3>(waypoints.Count);
            foreach (var waypoint in waypoints)
            {
                _waypoints.Add(waypoint.Position);
            }
        }
    }
}