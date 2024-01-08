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

            Experience.Instance.OnChangedExperience += InstanceOnOnChangedExperience;
        }

        private void InstanceOnOnChangedExperience(object sender, Experience.OnChangedExperienceEventArgs e)
        {
            shootBulletSo = Experience.Instance.GetCurrentExperienceStruct().shootBulletSo;
        }

        private void OnDestroy()
        {
            Experience.Instance.OnChangedExperience -= InstanceOnOnChangedExperience;
        }
        
        public void WeaponActionToPoint(Transform sourceTransform, Vector3 destinationPoint)
        {
            StartCoroutine(MoveBulletCoroutine(sourceTransform, destinationPoint, shootBulletSo.trajectory));
        }

        public float GetBulletReloadTime()
        {
            return shootBulletSo.bulletReloadTime;
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
                currentTrajectory == ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory
                &&
                shootBulletSo.alternateParabola != ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory
                &&
                shootBulletSo.alternateMinDistance > 0
                &&
                maxDistance < shootBulletSo.alternateMinDistance
                )
            {
                currentTrajectory = shootBulletSo.alternateParabola;
            }
            
            if (currentTrajectory == ShootBulletSo.ShootBulletTrajectory.ParallelTrajectory)
            {
                endPos.y = startPos.y;
                endPos = TransformHelper.NewEndPositionCalculateFromStartPosition(startPos, endPos, shootBulletSo.alternateMaxDistance);
                currentDirection = endPos - startPos;
                maxDistance = currentDirection.magnitude;
            }

            if (currentTrajectory == ShootBulletSo.ShootBulletTrajectory.LinerTrajectory)
            {
                currentDirection = endPos - startPos;
                maxDistance = currentDirection.magnitude;
            }
            
            float currentDistance = 0;
            while (currentDistance <= maxDistance)
            {
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

                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawLine(startPos, endPos, _debugProperties.debugLineColor, _debugProperties.debugDuration);
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
                DebugHelper.DrawCrossNormal(hit.point, hit.normal, 1f, _debugProperties.debugLongLineColor, _debugProperties.debugLongDuration);
                Debug.Log($"Raycast hit on {hit.collider.name} from {GetType()}");
            }
            
            TryHitEffect(hit);
                
            if (hit.collider.TryGetComponent(out IDamageable damage))
            {
                damage.TakeDamage(shootBulletSo.hitDamageVolume, hit.collider, shootBulletSo.hitDamageForce);
            }
            
            Destroy(gameObject);
        }
        
        private void TryHitEffect(RaycastHit hit)
        {
            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"TryHitEffect on point {hit.point}");
            }
            
            if (shootBulletSo.hitEffectPrefab) {
                if (_debugProperties.debugEnabled)
                {
                    Debug.Log($"Instantiate prefab and will destroy throw time: {shootBulletSo.hitEffectDestroyTime}");
                }
                Destroy(Instantiate(shootBulletSo.hitEffectPrefab, hit.point, Quaternion.identity), shootBulletSo.hitEffectDestroyTime);
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
        
        private void StartEffect(Transform sourceTransform)
        {
            if (shootBulletSo.startEffectPrefab) {
                Destroy(Instantiate(shootBulletSo.startEffectPrefab, sourceTransform.transform), shootBulletSo.startEffectDestroyTime);
            }
        }
    }
}