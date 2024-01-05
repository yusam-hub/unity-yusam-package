using UnityEngine;

namespace YusamPackage
{
    public class GameInputCursor : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Sprite cursorSprite;
        [SerializeField] private GameInputCursorUi gameInputCursorUiPrefab;
        [SerializeField] private Color cursorColor = Color.red;

        private GameInputCursorUi _gameInputCursorUi;
        private RectTransform _canvasRectTransform;

        private void Awake()
        {
            _canvasRectTransform = canvas.GetComponent<RectTransform>();
            _gameInputCursorUi = Instantiate(gameInputCursorUiPrefab, canvas.transform);
            _gameInputCursorUi.gameObject.SetActive(false);
        }

        private void DoUpdateImageSpriteColor()
        {
            if (_gameInputCursorUi)
            {
                _gameInputCursorUi.SetImage(cursorSprite);
                _gameInputCursorUi.SetImageColor(cursorColor);
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying && _gameInputCursorUi != null)
            {
                DoUpdateImageSpriteColor();
            }
        }

        private void OnEnable()
        {
            if (_gameInputCursorUi)
            {
                Cursor.visible = false;
                DoUpdateImageSpriteColor();
                _gameInputCursorUi.gameObject.SetActive(true);
            }
        }
        
        private void OnDisable()
        {
            if (_gameInputCursorUi)
            {
                Cursor.visible = true;
                _gameInputCursorUi.gameObject.SetActive(false);
            }
        }

        public RectTransform GetRectTransformCursor()
        {
            return _gameInputCursorUi.GetRectTransformCursor();
        }
        
        public RectTransform GetRectTransformCanvas()
        {
            return _canvasRectTransform;
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }
    }
}