using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class RotationByGamePadRightStick : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1000f;
        [SerializeField] private bool isometricEnabled;
        private GameInputController _gameInputController;

        private DebugProperties _debugProperties;
        
        
        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            _gameInputController = GetComponent<GameInputController>();
        }

        private void Update()
        {
            if (!_gameInputController.IsLayerAccessible()) return;

            RotateByRightStick();
        }

        private void RotateByRightStick()
        {
            Vector2 rightStick = _gameInputController.gameInput.GetRightStickDirection();
     
            Vector3 lookAt = Vector3.right * rightStick.x + Vector3.forward * rightStick.y;

            if (isometricEnabled)
            {
                Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                lookAt = isoMatrix.MultiplyPoint3x4(lookAt);
            }

            if (lookAt != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(lookAt, Vector3.up);

                transform.eulerAngles = Vector3.up *
                                        Mathf.MoveTowardsAngle(
                                            transform.eulerAngles.y,
                                            newRotation.eulerAngles.y,
                                            rotationSpeed * Time.deltaTime
                                        );
                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawRay(transform.position, lookAt, _debugProperties.debugLineColor, _debugProperties.debugDuration);

                }                
            }
        }
    }
}