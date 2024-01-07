using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class InputCharacterRotateByMousePoint : LookAtTargetPosition
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private float rayCastDistance = 100f;
        private GameInputController _gameInputController;

        private DebugProperties _debugProperties;
        private Vector3 _mousePosition;
        private Camera _camera;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            _gameInputController = GetComponent<GameInputController>();
            _camera = Camera.main;
        }

        private Vector2 GetInputMousePosition()
        {
            //return Input.mousePosition;
            return _gameInputController.gameInput.GetRightStickMousePosition();
        }
        
        private void Update()
        {
            if (!_gameInputController.IsLayerAccessible()) return;
            RotateToMouse();
        }

        private void RotateToMouse()
        {
            _mousePosition = MouseHelper.GetMouseLookPositionByRay(_mousePosition, GetInputMousePosition(), _camera, rayCastDistance);
        
            var lookAt = TransformHelper.LookAt(transform.position, _mousePosition);

            if (lookAt != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(
                    lookAt - new Vector3(transform.position.x, 0, transform.position.z)
                );
    

                transform.eulerAngles = Vector3.up *
                                        Mathf.MoveTowardsAngle(
                                            transform.eulerAngles.y, 
                                            targetRotation.eulerAngles.y, 
                                            rotationSpeed * Time.deltaTime
                                        );


                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawLine(transform.position, lookAt, _debugProperties.debugLineColor, _debugProperties.debugDuration);
                    var zeroY = new Vector3(lookAt.x, 0, lookAt.z);
                    
                    if (lookAt.y != 0)
                    {
                        Debug.DrawLine(lookAt, zeroY, _debugProperties.debugLineColor, _debugProperties.debugDuration);

                        if (lookAt != zeroY)
                        {
                            var rot = Quaternion.LookRotation(lookAt - zeroY);
                            DebugHelper.DrawCircle(zeroY, rot, 1, 8, _debugProperties.debugLineColor,
                                _debugProperties.debugDuration, true);
                            Debug.DrawLine(transform.position, zeroY, _debugProperties.debugLineColor,
                                _debugProperties.debugDuration);
                        }
                    }
                }
            }
        }

        public override Vector3 GetMousePositionAsVector3()
        {
            return _mousePosition;
        }
 
    }
}