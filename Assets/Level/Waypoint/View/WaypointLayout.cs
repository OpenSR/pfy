using PFY.Share;
using PFY.Utils;
using UnityEngine;

namespace PFY.Level.Waypoint.View
{
    public sealed class WaypointLayout : Layout
    {
        public Vector3 Position => transform.position;
        
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.yellow;
            var position = transform.position;
            Gizmos.DrawSphere(position, .1f);
            UtilsGizmos.DrawString(gameObject.name, position, Color.white, new Vector2(0, -1.5f), 10);
#endif
        }
    }
}