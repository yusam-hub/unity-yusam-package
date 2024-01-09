using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    [RequireComponent(typeof(GameInputController))]
    public class GameInputPerformed : MonoBehaviour
    {
        [SerializeField] private string comment;
        [SerializeField] private GameInputPerformedEnum[] performedInputArray;
        [SerializeField] private EmptyUnityEvent onPerformedInputEvent;

        private GameInputController _gameInputController;
        
        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
            
            foreach (GameInputPerformedEnum performedInput in performedInputArray)
            {
                _gameInputController.gameInput.GetActionByEnum(performedInput).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;

            onPerformedInputEvent?.Invoke(EventArgs.Empty);
        }

        private void OnDestroy()
        {
            foreach (GameInputPerformedEnum performedInput in performedInputArray)
            {
                _gameInputController.gameInput.GetActionByEnum(performedInput).performed -= OnInputAction;
            }
        }
    }
}