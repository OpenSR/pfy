using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PFY.Level.Bombs.Bomb.Types.Cluster.AttackUnit.View
{
    public sealed class ClusterBombAttackUnitView : IClusterBombAttackUnitView
    {
        bool IBombView.IsActive => _isActive;
        
        private bool _isActive;
        private ClusterBombAttackUnitLayout _layout;
        private AssetReference _prefabReference;

        public static IClusterBombAttackUnitView Create(AssetReference prefabReference, Transform parent, Vector3 position)
        {
            return new ClusterBombAttackUnitView(prefabReference, parent, position);
        }
        
        private ClusterBombAttackUnitView(AssetReference prefabReference, Transform parent, Vector3 position)
        {
            _isActive = false;
            _prefabReference = prefabReference;
            _prefabReference.InstantiateAsync(position, Quaternion.identity, parent).Completed += OnCompleted;
        }

        void IBombView.UpdatePosition(Vector3 position)
        {
            if (!_isActive)
            {
                return;
            }

            _layout.transform.position = position;
        }

        void IBombView.Destroy()
        {
            if (!_isActive)
            {
                return;
            }
            
            if (_layout)
            {
                _prefabReference.ReleaseInstance(_layout.gameObject);
            }
            
            _layout = null;
            _prefabReference = null;
        }
        
        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            obj.Completed -= OnCompleted;

            if (obj.Status == AsyncOperationStatus.Succeeded 
                && obj.IsDone
                && obj.Result.TryGetComponent(out _layout))
            {
                _isActive = true;
            }
        }
    }
}