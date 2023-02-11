using System;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PFY.Level.Bombs.Bomb.UI.View
{
    public sealed class BombUiView : IBombUiView
    {
        public event Action EventOnActivation;
        public event Action<IBombUiView> EventOnClick;

        bool IBombUiView.IsActive => _isActive;
        int IBombUiView.Id => _id;
        BombTypes IBombUiView.BombType => _bombType;

        private readonly int _id;
        private readonly BombTypes _bombType;
        
        private BombUiLayout _layout;
        private AssetReference _prefabReference;
        private AssetReference _iconReference;
        private RectTransform _parent;
        private bool _isActive;

        public static IBombUiView Create(int id, BombSettings bombSettings, RectTransform parent)
        {
            return new BombUiView(id, bombSettings, parent);
        }
        
        private BombUiView(int id, BombSettings bombSettings, RectTransform parent)
        {
            _id = id;
            _isActive = false;
            _prefabReference = bombSettings.prefabUiReference;
            _iconReference = bombSettings.iconReference;
            _parent = parent;
            _bombType = bombSettings.type;
        }

        void IBombUiView.Load()
        {
            _prefabReference.InstantiateAsync(_parent).Completed += OnCompleted;
        }

        void IBombUiView.Selected()
        {
            if (!_isActive)
            {
                return;
            }
            
            _layout.SetBackgroundColor = _layout.Select;
        }

        void IBombUiView.Unselected()
        {
            if (!_isActive)
            {
                return;
            }
            
            _layout.SetBackgroundColor = _layout.Normal;
        }

        void IBombUiView.Destroy()
        {
            if (!_isActive)
            {
                return;
            }

            if (_layout)
            {
                _prefabReference.ReleaseInstance(_layout.gameObject);
                _layout.EventOnClick -= LayoutOnEventOnClick;
            }

            _prefabReference = null;
            _layout = null;
            
            _iconReference.ReleaseAsset();
            _iconReference = null;

            _parent = null;
        }
        
        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            obj.Completed -= OnCompleted;

            if (obj.Status == AsyncOperationStatus.Succeeded 
                && obj.IsDone 
                && obj.Result.TryGetComponent(out _layout))
            {
                _iconReference.LoadAssetAsync<Sprite>().Completed += OnCompleted;
            }
        }

        private void OnCompleted(AsyncOperationHandle<Sprite> obj)
        {
            obj.Completed -= OnCompleted;

            if (obj.Status == AsyncOperationStatus.Succeeded && obj.IsDone)
            {
                _layout.SetIcon = obj.Result;
                _layout.EventOnClick += LayoutOnEventOnClick;
                _isActive = true;
                EventOnActivation?.Invoke();
            }
        }

        private void LayoutOnEventOnClick()
        {
            if (!_isActive)
            {
                return;
            }
            
            EventOnClick?.Invoke(this);
        }
    }
}