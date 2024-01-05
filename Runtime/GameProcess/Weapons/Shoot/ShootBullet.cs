using System;
using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private ShootBulletSo shootBulletSo;

        private DebugProperties _debugProperties;
        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }

        public void WeaponActionToPoint(Transform sourceTransform, Vector3 destinationPoint)
        {
            StartCoroutine(MoveBulletCoroutine(sourceTransform, destinationPoint, shootBulletSo.trajectory));
        }

        public float GetBulletReloadTime()
        {
            return shootBulletSo.bulletReloadTime;
        }
        
        private void StartEffect(Transform sourceTransform)
        {
            if (shootBulletSo.startEffectPrefab) {
                Destroy(
                    Instantiate(shootBulletSo.startEffectPrefab, sourceTransform.transform), shootBulletSo.startEffectDestroyTime
                );
            }
        }

        private IEnumerator MoveBulletCoroutine(Transform fromTransform, Vector3 toPosition, ShootBulletSo.ShootBulletTrajectory trajectory)
        {
            StartEffect(fromTransform);
            
            Vector3 currentDirection;
            float maxDistance;
            
            var startPos = fromTransform.position;
            var endPos = toPosition;
            
            var currentTrajectory = trajectory;

            currentDirection = endPos - startPos;
            maxDistance = currentDirection.magnitude;
            
            if (
                shootBulletSo.parallelMinDistance > 0
                &&
                currentTrajectory != ShootBulletSo.ShootBulletTrajectory.ParallelTrajectory
                &&
                maxDistance < shootBulletSo.parallelMinDistance
                )
            {
                currentTrajectory = ShootBulletSo.ShootBulletTrajectory.ParallelTrajectory;
            }

            if (currentTrajectory == ShootBulletSo.ShootBulletTrajectory.ParallelTrajectory)
            {
                endPos.y = startPos.y;
                endPos = TransformHelper.NewEndPositionCalculateFromStartPosition(startPos, endPos, shootBulletSo.parallelMaxDistance);
                currentDirection = endPos - startPos;
                maxDistance = currentDirection.magnitude;
            }


            //var lifeTimer = shootBulletSo.scriptLifeTime;
            float currentDistance = 0;
            while (currentDistance <= maxDistance)
            {
                /*lifeTimer -= Time.deltaTime;
                if (lifeTimer <= 0)
                {
                    SelfDestroy(null);
                    yield break; 
                }*/
                
                float currentTransition;
                Vector3 currentPosition;
                
                switch (currentTrajectory)
                {
                    case ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory:
                        currentTransition = currentDistance / maxDistance;
                        var parabolicHeight = 4f * shootBulletSo.parabolaHeight * currentTransition * ( 1f - currentTransition);
                        currentPosition = Vector3.Lerp(startPos, endPos, currentTransition);
                        currentPosition.y += parabolicHeight;
                        break;
                    case ShootBulletSo.ShootBulletTrajectory.LinerTrajectory:
                        currentTransition = currentDistance / maxDistance;
                        currentPosition = Vector3.Lerp(startPos, endPos, currentTransition);
                        break;
                    default:
                        currentTransition = currentDistance / maxDistance;
                        currentPosition = Vector3.Lerp(startPos, endPos, currentTransition);
                        break;                        
                }
                
                if (shootBulletSo.rotateToTrajectory) {
                    var lookDir = currentPosition - transform.position;
                    if (lookDir.sqrMagnitude >= Mathf.Epsilon) {
                        transform.rotation = Quaternion.LookRotation(lookDir);
                    }
                }

                RaycastHit? hitTest;
                hitTest = TryGetHitInfo(transform.position, currentPosition);
                if (hitTest.HasValue) {
                    HitAndSelfDestroy(hitTest.Value);
                    yield break;
                }

                transform.position = currentPosition;
                currentDistance += shootBulletSo.bulletSpeed * Time.deltaTime;

                yield return null;
            }

            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"Raycast not found for [ Hit Damage Layer Mask ] in scriptable object [ {shootBulletSo.name} ]");
            }

            Destroy(gameObject);
        }
        
        private void HitAndSelfDestroy(RaycastHit hit)
        {
            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"Raycast hit on {hit.collider.name} from {GetType()}");
            }
            
            TryHitEffect(hit.point);
                
            if (hit.collider.TryGetComponent(out IDamage damage))
            {
                damage.DoDamage(hit.collider, shootBulletSo.hitDamageVolume, shootBulletSo.hitDamageForce);
            }
            
            Destroy(gameObject);
        }
        
        private void TryHitEffect(Vector3 point)
        {
            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"TryHitEffect on point {point}");
            }
            
            if (shootBulletSo.hitEffectPrefab) {
                if (_debugProperties.debugEnabled)
                {
                    Debug.Log($"Instantiate prefab and will destroy throw time: {shootBulletSo.hitEffectDestroyTime}");
                }
                Destroy(
                    Instantiate(shootBulletSo.hitEffectPrefab, point, Quaternion.identity), shootBulletSo.hitEffectDestroyTime
                );
            }
            else
            {
                if (_debugProperties.debugEnabled)
                {
                    Debug.Log($"Hit Effect Prefab not found in {typeof(ShootBulletSo)}");
                }

            }
        }
        
        private RaycastHit? TryGetHitInfo(Vector3 fromPosition, Vector3 toPosition)
        {
            var castRay = new Ray( fromPosition, toPosition - fromPosition );

            if (Physics.SphereCast(
                    castRay, 
                    shootBulletSo.bulletHitRadius, 
                    out var hitInfo
                    ,( fromPosition - toPosition ).magnitude
                    ,shootBulletSo.hitDamageLayerMask
                    )
                ) {
                return hitInfo;
            }
            
            return null;
        }
    }
}