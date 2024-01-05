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
            if (!_gameInputController.IsLayerAccessible()) return;
            
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
            transform.Translate(_gameInputController.gameInput.GetLeftStickDirectionAsVector3() * moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            var rightStickDirection = _gameInputController.gameInput.GetMouseDirection();
            var input = new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
            input *= rotateSpeed;
            input *= Time.deltaTime;
            transform.Rotate( input);
            var eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}