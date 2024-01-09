using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    [DisallowMultipleComponent]
    public class InputShootController : MonoBehaviour
    {
        [SerializeField] private GameInputController gameInputController;
        [SerializeField] private GameInputPerformedEnum[] inputs;
        [SerializeField] private EmptyUnityEvent onShootEvent;

        private void Awake()
        {
            foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!gameInputController.IsLayerAccessible()) return;
            Debug.Log("OnInputAction");
            onShootEvent?.Invoke(EventArgs.Empty);
        }

        private void OnDestroy()
        {
            foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}