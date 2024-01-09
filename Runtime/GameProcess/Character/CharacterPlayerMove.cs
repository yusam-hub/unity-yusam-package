using UnityEngine;
using UnityEngine.Serialization;

namespace YusamPackage
{
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class CharacterPlayerMove : MonoBehaviour
    {
        [SerializeField] private GameInputDirection gameInputDirection;
        [SerializeField] private float moveSpeed = 8;
        [SerializeField] private bool isometricEnabled;
        
        private CharacterController _characterController;
        private GameInputController _gameInputController;
        private Movable _movable;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _gameInputController = GetComponent<GameInputController>();
            _movable = GetComponent<Movable>();
        }
        
        private void Update()
        {
            if (!_gameInputController.IsLayerAccessible()) return;
            CharacterControllerMove();
        }
        
        private void CharacterControllerMove()
        {
            if (!_movable.CanMoving()) return;
            
            var input2 = gameInputDirection.GetInputDirection();
            var input = new Vector3(input2.x, 0, input2.y);
            
            if (isometricEnabled)
            {
                Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                input = isoMatrix.MultiplyPoint3x4(input);
            }
        
            var move = input;
            move *= Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1 ? .7f : 1;
            move *= moveSpeed;
            move += Vector3.up * -8;
            _characterController.Move(move * Time.deltaTime);
        }
    }
}