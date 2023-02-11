using Leopotam.EcsLite;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Base.Components
{
    public struct ComponentBombDetonationPosition : IEcsAutoReset<ComponentBombDetonationPosition>
    {
        public Vector3 Value;
        
        public void AutoReset(ref ComponentBombDetonationPosition c)
        {
            c.Value = Vector3.zero;
        }
    }
}