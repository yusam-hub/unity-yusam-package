using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class CharacterRotateToTransform : LookAtTargetPosition
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private GameObject target;
        [SerializeField] private string findGameObjectWithTag;
        [SerializeField] private Vector3 positionOffset;
        
        private Vector3 _lookPosition;
        
        private void Awake()
        {
            if (target == null)
            {
                FindTarget();
            }
        }

        private void FindTarget()
        {
            if (target != null) return;
            if (findGameObjectWithTag != "")
            {
                target = GameObject.FindGameObjectWithTag(findGameObjectWithTag);
            }
        }

        private Vector3 GetTargetTransformPosition()
        {
            var pos = target.transform.position;
            pos.y += positionOffset.y;
            return pos;
        }

        private void Update()
        {
            if (!target) return;
            
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

        public override Vector3 GetLookAtTargetPosition()
        {
            if (target)
            {
                return GetTargetTransformPosition();
            }
            return Vector3.zero;
        }
    }
}