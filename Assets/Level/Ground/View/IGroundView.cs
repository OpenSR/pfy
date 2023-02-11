using UnityEngine;

namespace PFY.Level.Ground.View
{
    public interface IGroundView
    {
        Transform GroundTransform { get; }
        
        void Destroy();
    }
}