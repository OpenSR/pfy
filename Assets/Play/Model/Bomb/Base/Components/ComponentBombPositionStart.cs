using Leopotam.EcsLite;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombPositionStart : IEcsAutoReset<ComponentBombPositionStart>
    {
        public Vector3 Value;

        public void AutoReset(ref ComponentBombPositionStart c)
        {
            c.Value = Vector3.zero;
        }
    }
}