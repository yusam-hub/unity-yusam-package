using System;
using UnityEngine;

namespace YusamPackage.GameInput
{
    public class FlyCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 50;
        [SerializeField] private float moveSpeed = 5;

        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;

        private void Awake()
        {
            if (GameInput.Instance == null)
            {
                Debug.LogError("GameInput instance not found! " + this);
            }
        }

        /*
         * Controlled by GameInputManager 
         */
        private bool IsButtonPressed()
        {
            if (GameInput.Instance == null)
            {
                return false;
            }
            return GameInput.Instance.GetMouseRightPressAction().IsPressed() 
                   || 
                   GameInput.Instance.GetLeftTriggerPressAction().IsPressed();
        }
        private Vector3 GetLeftStickDirection()
        {
            if (GameInput.Instance == null)
            {
                return Vector3.zero;
            }
            Vector2 leftStickDirection = GameInput.Instance.GetLeftStickVector2Normalized();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        private Vector3 GetRightStickDirection()
        {
            if (GameInput.Instance == null)
            {
                return Vector3.zero;
            }
            Vector2 rightStickDirection = GameInput.Instance.GetRightStickVector2Normalized();
            return new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
        }
        
        /*
         * 
         */
        private void Update()
        {
            /*
             * todo: need state machine
             */
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
            transform.Translate(GetLeftStickDirection() * moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            transform.Rotate(GetRightStickDirection() * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}