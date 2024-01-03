using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class FlyMainCameraInPerspectiveMode : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float rotateSpeed = 50;
        [SerializeField] private float moveSpeed = 5;

        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;

        private GameInputController _gameInputController;

        private void Awake()
        {
            _gameInputController = GetComponent<GameInputController>();
        }

        /*
         * Controlled by GameInputManager
         */
        private bool IsButtonPressed()
        {
            return _gameInputController.gameInput.GetMouseRightPressAction().IsPressed() 
                   || 
                   _gameInputController.gameInput.GetLeftTriggerPressAction().IsPressed();
        }
        /*
         * 
         */
        private void Update()
        {
            if (!_gameInputController.CanUseGameInput()) return;
            
            if (IsButtonPressed())
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UpdateMovement();
                UpdateRotation();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }  
        }
        
        private void UpdateMovement()
        {
            transform.Translate(_gameInputController.gameInput.GetLeftStickDirection3() * moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            Vector2 rightStickDirection = _gameInputController.gameInput.GetMouseDirection();
            Vector3 input = new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
            
            transform.Rotate( input * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}