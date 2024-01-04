using System;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage
{
    public class GameInputCursor : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Sprite cursorSprite;
        [SerializeField] private GameInputCursorUi gameInputCursorUiPrefab;
        [SerializeField] private Color cursorColor = Color.red;

        private GameInputCursorUi _gameInputCursorUi;
        private void Awake()
        {
            if (gameInputCursorUiPrefab != null && cursorSprite != null && canvas != null)
            {
                _gameInputCursorUi = Instantiate(gameInputCursorUiPrefab, canvas.transform);
                _gameInputCursorUi.gameObject.SetActive(false);
            }
        }

        public void DoUpdateImageSpriteColor()
        {
            if (_gameInputCursorUi)
            {
                _gameInputCursorUi.SetImage(cursorSprite);
                _gameInputCursorUi.SetImageColor(cursorColor);
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                DoUpdateImageSpriteColor();
            }
        }

        private void OnEnable()
        {
            if (_gameInputCursorUi != null)
            {
                DoUpdateImageSpriteColor();
                _gameInputCursorUi.gameObject.SetActive(true);
            }
        }
        
        private void OnDisable()
        {
            if (_gameInputCursorUi != null)
            {
                _gameInputCursorUi.gameObject.SetActive(false);
            }
        }

        public RectTransform GetRectTransformCursor()
        {
            return _gameInputCursorUi.GetRectTransformCursor();

        }
        
        public RectTransform GetRectTransformCanvas()
        {
            return canvas.GetComponent<RectTransform>();
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }
    }
}