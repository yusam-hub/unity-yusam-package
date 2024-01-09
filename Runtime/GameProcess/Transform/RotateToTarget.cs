using UnityEngine;

namespace YusamPackage
{
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Target))]
    public class RotateToTarget : LookAtTargetPosition
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private string findGameObjectWithTag;
        [SerializeField] private Vector3 positionOffset;
        
        private Vector3 _lookPosition;
        private Target _target;
        
        private void Awake()
        {
            _target = GetComponent<Target>();
            if (_target.GetTarget() == null)
            {
                FindTarget();
            }
        }

        private void FindTarget()
        {
            if (_target.GetTarget() != null) return;
            if (findGameObjectWithTag != "")
            {
                _target.SetTarget(GameObject.FindGameObjectWithTag(findGameObjectWithTag)?.transform);
            }
        }

        private Vector3 GetTargetTransformPosition()
        {
            var pos = _target.GetTarget().transform.position;
            pos.y += positionOffset.y;
            return pos;
        }

        private void Update()
        {
            if (!_target.GetTarget()) return;
            
            var lookAt = TransformHelper.LookAt(transform.position, GetTargetTransformPosition());

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

            }
        }

        public override Vector3 GetMousePositionAsVector3()
        {
            if (_target.GetTarget())
            {
                return GetTargetTransformPosition();
            }
            return Vector3.zero;
        }
    }
}