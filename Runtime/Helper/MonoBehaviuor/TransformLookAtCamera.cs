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
            CameraForwardInverted
        }

        [SerializeField] private Mode mode;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            LogErrorHelper.NotFoundWhatInIf(_camera == null, typeof(Camera).ToString(), this);
        }

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(_camera.transform);
                    break;
                case Mode.LookAtInverted:
                    var pos = transform.position;
                    var dirFromCamera = pos - _camera.transform.position;
                    transform.LookAt(pos + dirFromCamera);
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
