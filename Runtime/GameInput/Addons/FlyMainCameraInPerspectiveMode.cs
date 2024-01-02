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
            transform.Translate(_gameInputController.GetLeftStickDirection() * moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            transform.Rotate(_gameInputController.GetRightStickDirection() * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}