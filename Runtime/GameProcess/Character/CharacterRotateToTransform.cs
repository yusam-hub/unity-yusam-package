using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    [DisallowMultipleComponent]
    public class CharacterRotateToTransform : LookAtTargetPosition
    {
        [SerializeField] private float rotationSpeed = 450;
        [SerializeField] private Transform rotateTo;
        
        private DebugProperties _debugProperties;
        private Vector3 _lookPosition;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            LogErrorHelper.NotFoundWhatInIf(rotateTo == null,typeof(Transform) + " : Nozzle Point", this);
        }

        private void Update()
        {
            if (!rotateTo) return;
            
            var lookAt = TransformHelper.LookAt(transform.position, rotateTo.position);

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
            return rotateTo.position;
        }
 }
}