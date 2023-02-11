using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PFY.Level.Bombs.Bomb.Types.Ordinary.View
{
    public sealed class OrdinaryBombView : IOrdinaryBombView
    {
        bool IBombView.IsActive => _isActive;
        
        private bool _isActive;
        private OrdinaryBombLayout _layout;
        private AssetReference _prefabReference;

        public static IOrdinaryBombView Create(AssetReference prefabReference, Transform parent, Vector3 position)
        {
            return new OrdinaryBombView(prefabReference, parent, position);
        }
        
        private OrdinaryBombView(AssetReference prefabReference, Transform parent, Vector3 position)
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