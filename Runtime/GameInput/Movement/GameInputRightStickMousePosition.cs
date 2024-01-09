using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class GameInputRightStickMousePosition : GameInputPosition
    {
        private GameInputController _gameInputController;
        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
        }
        
        public override Vector2 GetInputPosition()
        {
            return _gameInputController.gameInput.GetRightStickMousePosition();
        }
    }
}