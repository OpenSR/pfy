using UnityEngine;

namespace PFY.Level.Bombs.Bomb.Types
{
    public interface IBombView
    {
        bool IsActive { get; }

        void UpdatePosition(Vector3 position);
        void Destroy();
    }
}