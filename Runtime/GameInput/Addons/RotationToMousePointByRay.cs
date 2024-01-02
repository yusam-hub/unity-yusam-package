using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(YusamDebugProperties))]
    public class RotationToMousePointByRay : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 450;

        private YusamDebugProperties _debugProperties;
        private Vector3 _lookPosition;
        private Camera _camera;
        private Quaternion _targetRotation;
        
        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
            _camera = Camera.main;
        }

        private void Update()
        {
            RotateToMouse();
        }

        private void RotateToMouse()
        {
            _lookPosition = MouseHelper.GetMouseLookPositionByRay(_lookPosition, Input.mousePosition, _camera, 100);
        
            Vector3 lookAt = TransformHelper.LookAt(transform.position, _lookPosition);
        
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
}