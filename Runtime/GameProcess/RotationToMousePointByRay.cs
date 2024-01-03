﻿using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class RotationToMousePointByRay : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private float rayCastDistance = 100f;
        private GameInputController _gameInputController;

        private YusamDebugProperties _debugProperties;
        private Vector3 _lookPosition;
        private Camera _camera;
        private Quaternion _targetRotation;
        
        
        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
            _gameInputController = GetComponent<GameInputController>();
            _camera = Camera.main;
            if (_camera == null)
            {
                Debug.LogError("Camera.main instance not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
        }

        private Vector3 GetInputMousePosition()
        {
            //return Input.mousePosition;
            return _gameInputController.gameInput.GetMousePosition();
        }
        
        private void Update()
        {
            if (!_gameInputController.CanUseGameInput()) return;
            RotateToMouse();
        }

        private void RotateToMouse()
        {
            _lookPosition = MouseHelper.GetMouseLookPositionByRay(_lookPosition, GetInputMousePosition(), _camera, rayCastDistance);
        
            Vector3 lookAt = TransformHelper.LookAt(transform.position, _lookPosition);

            if (lookAt != Vector3.zero)
            {
                _targetRotation = Quaternion.LookRotation(
                    lookAt - new Vector3(transform.position.x, 0, transform.position.z)
                );
    

                transform.eulerAngles = Vector3.up *
                                        Mathf.MoveTowardsAngle(
                                            transform.eulerAngles.y, 
                                            _targetRotation.eulerAngles.y, 
                                            rotationSpeed * Time.deltaTime
                                        );


                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawLine(transform.position, lookAt, _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
                    Vector3 zeroY = new Vector3(lookAt.x, 0, lookAt.z);
                    
                    if (lookAt.y != 0)
                    {
                        Debug.DrawLine(lookAt, zeroY, _debugProperties.debugDefaultColor,
                            _debugProperties.debugDefaultDuration);
                    }
                    DebugHelper.DrawCircleXZ( zeroY, 1, 8, _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
                }
            }
        }

        public Vector3 GetMouseLookPosition()
        {
            return _lookPosition;
        }
 
    }
}