using System;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage
{
    [RequireComponent(typeof(Image))]
    public class GameInputCursorUi : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        
        public void SetImageColor(Color color)
        {
            _image.color = color;
        }

        public RectTransform GetRectTransformCursor()
        {
            return _rectTransform;
        }
    }
}