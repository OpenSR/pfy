using PFY.Share;
using PFY.Utils;
using UnityEngine;

namespace PFY.Level.Wall.View
{
    public sealed class WallLayout : Layout
    {
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            UtilsGizmos.DrawString(gameObject.name, transform.position, Color.white, Vector2.zero, 10);
#endif
        }
    }
}