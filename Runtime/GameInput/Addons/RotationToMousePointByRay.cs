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
            _lookPosition = GetMouseLookPositionByRay(_lookPosition, Input.mousePosition, _camera, 100);
        
            Vector3 lookAt = LookAt(transform.position, _lookPosition);
        
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
            }
        }
        
        public static Vector3 GetMouseLookPositionByRay(Vector3 lookPosition, Vector3 inputMousePosition, Camera camera, float distance, int layerMask)
        {
            Ray ray = camera.ScreenPointToRay(inputMousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
            {
                lookPosition = hit.point;
            }
            
            return lookPosition;
        }
        
        public static Vector3 GetMouseLookPositionByRay(Vector3 lookPosition, Vector3 inputMousePosition, Camera camera, float distance)
        {
            Ray ray = camera.ScreenPointToRay(inputMousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                lookPosition = hit.point;
            }
            
            return lookPosition;
        }

        public static Vector3 LookAt(Vector3 sourcePosition, Vector3 lookPosition)
        {
            return sourcePosition + (lookPosition - sourcePosition);
        }
    }
}