using System;
using PFY.Share;
using UnityEngine;
using UnityEngine.UI;

namespace PFY.Meta.View
{
    public sealed class MetaLayout : Layout
    {
        public event Action EventOnPlayButtonClick;
        
        [SerializeField]
        private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            EventOnPlayButtonClick?.Invoke();
        }
    }
}