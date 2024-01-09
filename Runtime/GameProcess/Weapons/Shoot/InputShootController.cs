using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class InputShootController : MonoBehaviour
    {
        [SerializeField] private GameInputPerformedEnum[] inputs;
        [SerializeField] private EmptyUnityEvent onShootEvent;

        private GameInputController _gameInputController;
        
        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
            
            foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;

            onShootEvent?.Invoke(EventArgs.Empty);
        }

        private void OnDestroy()
        {
            foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}