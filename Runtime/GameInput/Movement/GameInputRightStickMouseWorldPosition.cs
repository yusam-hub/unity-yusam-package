using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class GameInputRightStickMouseWorldPosition : GameInputWorldPosition
    {
        [SerializeField] private Camera currentCamera;
        [SerializeField] private float rayCastDistance = 100f;

        private GameInputController _gameInputController;
        private Vector3 _lastInputWorldPosition;
        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
            if (currentCamera == null)
            {
                currentCamera = Camera.main;
            }
        }
        
        public override Vector3 GetInputWorldPosition()
        {
            var ray = currentCamera.ScreenPointToRay(_gameInputController.gameInput.GetRightStickMousePosition());

            if (Physics.Raycast(ray, out var hit, rayCastDistance))
            {
                _lastInputWorldPosition = hit.point;
            }
            
            return _lastInputWorldPosition;
        }
    }
}