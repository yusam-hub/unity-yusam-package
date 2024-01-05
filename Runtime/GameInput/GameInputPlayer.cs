using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    public class GameInputPlayer : MonoBehaviour
    {
        [SerializeField] private GameInputPerformedEnum[] inputs;

        [SerializeField] private UnityEvent voidOutputs = new();
        
        private GameInputController _gameInputController;

        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();

            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;
 
            voidOutputs?.Invoke();
        }

        private void OnDestroy()
        {
            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}