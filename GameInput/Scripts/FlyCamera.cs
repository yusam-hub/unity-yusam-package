using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameInput.Scripts
{
    public class FlyCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 50;
        [SerializeField] private float moveSpeed = 5;

        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;

        private bool IsButtonPressed()
        {
            return GameInputManager.Instance.GetMouseRightPressAction().IsPressed() || GameInputManager.Instance.GetLeftTriggerPressAction().IsPressed();
        }
        
        private Vector3 GetLeftStickDirection()
        {
            Vector2 leftStickDirection = GameInputManager.Instance.GetLeftStickVector2Normalized();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        
        private Vector3 GetRightStickDirection()
        {
            /*Vector2 mouseDirection = GameInputManager.Instance.GetMouseNormalized();
            if (mouseDirection != Vector2.zero)
            {
                return new Vector3(-mouseDirection.y, mouseDirection.x, 0);       
            }*/

            Vector2 rightStickDirection = GameInputManager.Instance.GetRightStickVector2Normalized();
            return new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
        }
        
        private void Update()
        {
            if (IsButtonPressed())
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Movement();
                Rotation();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }  
        }
        
        private void Movement()
        {
            transform.Translate(GetLeftStickDirection() * moveSpeed * Time.deltaTime);
        }

        private void Rotation()
        {
            transform.Rotate(GetRightStickDirection() * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}