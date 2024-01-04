using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class MovementCharacterController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8;
        [SerializeField] private bool isometricEnabled;
        
        private CharacterController _characterController;
        private GameInputController _gameInputController;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _gameInputController = GetComponent<GameInputController>();
        }
        
        private void Update()
        {
            if (!_gameInputController.CanUseGameInput()) return;
            Movement();
        }

        private Vector3 GetInputMovement()
        {
            //return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            return _gameInputController.gameInput.GetLeftStickDirectionAsVector3();
        }
        
        private void Movement()
        {
            Vector3 input = GetInputMovement();
            
            if (isometricEnabled)
            {
                Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                input = isoMatrix.MultiplyPoint3x4(input);
            }
        
            Vector3 move = input;
            move *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
            move *= moveSpeed;
            move += Vector3.up * -8;
            _characterController.Move(move * Time.deltaTime);
        }
    }
}