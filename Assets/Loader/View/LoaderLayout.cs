using PFY.Share;
using TMPro;
using UnityEngine;

namespace PFY.Loader.View
{
    public sealed class LoaderLayout : Layout
    {
        public string Header
        {
            set
            {
                if (_isInit && header)
                {
                    header.text = value ?? string.Empty;
                }
            }
        }

        public int Progress
        {
            set
            {
                if (_isInit && progress)
                {
                    progress.text = $"{Mathf.Max(0, Mathf.Min(100, value))}%";
                }
            }
        }
        
        [SerializeField]
        private TMP_Text header;
        [SerializeField]
        private TMP_Text progress;

        private bool _isInit;
        
        private void Awake()
        {
            _isInit = true;
        }

        private void OnDestroy()
        {
            _isInit = false;
        }

        public void Show()
        {
            if (!gameObject || gameObject.activeSelf)
            {
                return;
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (!gameObject || !gameObject.activeSelf)
            {
                return;
            }

            gameObject.SetActive(false);
        }
    }
}