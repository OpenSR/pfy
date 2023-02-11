using Leopotam.EcsLite;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Drop.Components
{
    public struct ComponentBombDropTargetPosition : IEcsAutoReset<ComponentBombDropTargetPosition>
    {
        public Vector3 TargetPosition;
        
        public void AutoReset(ref ComponentBombDropTargetPosition c)
        {
            c.TargetPosition = Vector3.zero;
        }
    }
}