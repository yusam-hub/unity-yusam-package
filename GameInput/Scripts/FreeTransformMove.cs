using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameInput.Scripts
{
    public class FreeTransformMove : MonoBehaviour
    {

        private enum TypeInputEnum
        {
            GameInput,
            OldInput
        }
        private enum TypeMoveEnum
        {
            Camera,
            Player
        }
        
        [SerializeField] private TypeInputEnum typeInputEnum = TypeInputEnum.GameInput;
        [SerializeField] private TypeMoveEnum typeMoveEnum = TypeMoveEnum.Camera;
       
        [SerializeField] private float rotateSpeed = 50;
        [SerializeField] private float moveSpeed = 5;

        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;

        private bool IsButtonPressed()
        {
            if (typeInputEnum == TypeInputEnum.GameInput)
            {
                return GameInputManager.Instance.GetMouseRightPressAction().IsPressed() || GameInputManager.Instance.GetLeftTriggerPressAction().IsPressed();
            }
            return Input.GetMouseButton(1);
        }
        
        private Vector3 GetLeftStickDirection()
        {
            if (typeInputEnum == TypeInputEnum.GameInput)
            {
                Vector2 leftStickDirection = GameInputManager.Instance.GetLeftStickVector2Normalized();
                return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
            }
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        
        private Vector3 GetRightStickDirection()
        {
            if (typeInputEnum == TypeInputEnum.GameInput)
            {
                Vector2 mouseDirection = GameInputManager.Instance.GetMouseNormalized();
                if (mouseDirection != Vector2.zero)
                {
                    return new Vector3(-mouseDirection.y, mouseDirection.x, 0);       
                }

                Vector2 rightStickDirection = GameInputManager.Instance.GetRightStickVector2Normalized();
                return new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
            }
            return new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
        
        private Vector3 ConvertToIfPlayer(Vector3 direction)
        {
            if (typeMoveEnum == TypeMoveEnum.Player)
            {
                return new Vector3(0, direction.y,0);
            }

            return direction;
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
            transform.Rotate(ConvertToIfPlayer(GetRightStickDirection()) * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

    }
}