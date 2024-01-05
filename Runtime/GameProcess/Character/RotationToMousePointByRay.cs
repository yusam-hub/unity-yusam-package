﻿using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class RotationToMousePointByRay : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private float rayCastDistance = 100f;
        private GameInputController _gameInputController;

        private DebugProperties _debugProperties;
        private Vector3 _lookPosition;
        private Camera _camera;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            _gameInputController = GetComponent<GameInputController>();
            _camera = Camera.main;
        }

        private Vector3 GetInputMousePosition()
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
            _lookPosition = MouseHelper.GetMouseLookPositionByRay(_lookPosition, GetInputMousePosition(), _camera, rayCastDistance);
        
            var lookAt = TransformHelper.LookAt(transform.position, _lookPosition);

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
                    }
                    DebugHelper.DrawCircleXZ( zeroY, 1, 8, _debugProperties.debugLineColor, _debugProperties.debugDuration);
                }
            }
        }

        public Vector3 GetMouseLookPosition()
        {
            return _lookPosition;
        }
 
    }
}