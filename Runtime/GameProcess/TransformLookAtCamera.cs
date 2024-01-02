using System;
using UnityEngine;

namespace YusamPackage
{
    public class TransformLookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] private Mode mode;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            if (_camera == null)
            {
                Debug.LogError("Camera.main instance not found in [ " + this + "]");
                gameObject.SetActive(false);
            }            
        }

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(_camera.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - _camera.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = _camera.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -1 * _camera.transform.forward;
                    break;
            }

        }
    }
}
