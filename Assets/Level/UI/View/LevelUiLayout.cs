using System;
using PFY.Level.Bombs.BombSelector.UI.View;
using PFY.Share;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFY.Level.UI.View
{
    public sealed class LevelUiLayout : Layout
    {
        public event Action EventOnGameEndButtonClick;
        
        public string LevelLabel
        {
            get => levelLabel.text;
            set => levelLabel.text = value;
        }
        public RectTransform LevelLabelRectTransform => levelLabel.rectTransform;
        public BombSelectorUiLayout BombSelectorUiLayout => bombSelectorUi;

        [SerializeField]
        private TMP_Text levelLabel;
        [SerializeField]
        private Button gameEnd;
        [SerializeField]
        private BombSelectorUiLayout bombSelectorUi;

        private void Awake()
        {
            gameEnd.onClick.AddListener(OnGameEndButtonClick);
        }

        private void OnDestroy()
        {
            gameEnd.onClick.RemoveListener(OnGameEndButtonClick);
        }

        private void OnGameEndButtonClick()
        {
            EventOnGameEndButtonClick?.Invoke();
        }
    }
}