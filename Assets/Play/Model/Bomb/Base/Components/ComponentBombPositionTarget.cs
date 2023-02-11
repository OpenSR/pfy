using Leopotam.EcsLite;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombPositionTarget : IEcsAutoReset<ComponentBombPositionTarget>
    {
        public Vector3 Value;

        public void AutoReset(ref ComponentBombPositionTarget c)
        {
            c.Value = Vector3.zero;
        }
    }
}