using System;
using PFY.Share;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PFY.Level.Ground.View
{
    public sealed class GroundLayout : Layout, IPointerClickHandler
    {
        public event Action<Vector3> EventOnClick; 

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            EventOnClick?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }
    }
}