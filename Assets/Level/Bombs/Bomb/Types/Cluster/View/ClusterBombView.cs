using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PFY.Level.Bombs.Bomb.Types.Cluster.View
{
    public sealed class ClusterBombView : IClusterBombView
    {
        bool IBombView.IsActive => _isActive;
        
        private bool _isActive;
        private ClusterBombLayout _layout;
        private AssetReference _prefabReference;

        public static IClusterBombView Create(AssetReference prefabReference, Transform parent, Vector3 position)
        {
            return new ClusterBombView(prefabReference, parent, position);
        }
        
        private ClusterBombView(AssetReference prefabReference, Transform parent, Vector3 position)
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