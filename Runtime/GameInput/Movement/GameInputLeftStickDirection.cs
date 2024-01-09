using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class GameInputLeftStickDirection : GameInputDirection
    {
        private GameInputController _gameInputController;
        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
        }
        
        public override Vector2 GetInputDirection()
        {
            return _gameInputController.gameInput.GetLeftStickDirection();
        }
    }
}