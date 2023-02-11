using System;
using PFY.Share;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PFY.Level.Bombs.Bomb.UI.View
{
    public sealed class BombUiLayout : Layout, IPointerClickHandler
    {
        public event Action EventOnClick;
        
        public Color Normal => normal;
        public Color Select => select;

        public Color SetBackgroundColor
        {
            set => background.color = value;
        }

        public Sprite SetIcon
        {
            get => icon.sprite;
            set => icon.sprite = value;
        }
        
        [SerializeField]
        private Color normal;
        [SerializeField]
        private Color select;
        [SerializeField]
        private Image background;
        [SerializeField]
        private Image icon;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            EventOnClick?.Invoke();
        }
    }
}