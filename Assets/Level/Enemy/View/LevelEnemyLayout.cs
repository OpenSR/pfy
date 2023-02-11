using PFY.Share;
using UnityEngine;
using UnityEngine.AI;

namespace PFY.Level.Enemy.View
{
    public sealed class LevelEnemyLayout : Layout
    {
        public float PosY => posY;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        
        [SerializeField]
        private float posY;
        [SerializeField]
        private NavMeshAgent navMeshAgent;
    }
}