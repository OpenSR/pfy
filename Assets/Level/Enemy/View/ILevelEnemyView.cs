using UnityEngine;
using UnityEngine.AI;

namespace PFY.Level.Enemy.View
{
    public interface ILevelEnemyView
    {
        bool IsActive { get; }
        Vector3 Position { get; }
        NavMeshAgent Navigation { get; }
        
        void Destroy();
    }
}