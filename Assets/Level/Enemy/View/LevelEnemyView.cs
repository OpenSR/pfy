using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PFY.Level.Enemy.View
{
    public sealed class LevelEnemyView : ILevelEnemyView
    {
        bool ILevelEnemyView.IsActive => _isActive;
        Vector3 ILevelEnemyView.Position => _layout.transform.position;
        NavMeshAgent ILevelEnemyView.Navigation => _layout.NavMeshAgent;

        private bool _isActive;
        private LevelEnemyLayout _layout;
        private AssetReference _prefab;
        private float _moveSpeed;

        public static ILevelEnemyView Create(Transform parent, AssetReference prefab, float moveSpeed, Vector3 position)
        {
            return new LevelEnemyView(parent, prefab, moveSpeed, position);
        }

        private LevelEnemyView(Transform parent, AssetReference prefab, float moveSpeed, Vector3 position)
        {
            _isActive = false;
            _moveSpeed = moveSpeed;
            _prefab = prefab;
            _prefab.InstantiateAsync(position, Quaternion.identity, parent).Completed += OnCompleted;
        }

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            obj.Completed -= OnCompleted;

            if (obj.Status == AsyncOperationStatus.Succeeded 
                && obj.IsDone
                && obj.Result.TryGetComponent(out _layout))
            {
                _layout.NavMeshAgent.autoBraking = false;
                _layout.NavMeshAgent.speed = _moveSpeed;
                _isActive = true;
            }
        }

        void ILevelEnemyView.Destroy()
        {
#if UNITY_EDITOR || DEBUG
            Debug.Log("Call enemy view destroy.");
#endif

            if (_layout)
            {
                _prefab?.ReleaseInstance(_layout.gameObject);
            }

            _prefab = null;
        }
    }
}