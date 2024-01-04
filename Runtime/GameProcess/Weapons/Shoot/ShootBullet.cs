﻿using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    public class ShootBullet : MonoBehaviour, IWeaponActionToPoint, IShootBullet
    {
        [SerializeField] private ShootBulletSo shootBulletSo;

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
            
            Vector3 startPos = fromTransform.position;
            Vector3 endPos = toPosition;
            ShootBulletSo.ShootBulletTrajectory currentTrajectory = trajectory;

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


            float lifeTimer = shootBulletSo.scriptLifeTime;
            float currentDistance = 0;
            while (currentDistance <= maxDistance)
            {
                lifeTimer -= Time.deltaTime;
                if (lifeTimer <= 0)
                {
                    SelfDestroy(null);
                    yield break; 
                }
                
                float currentTransition;
                Vector3 currentPosition;
                
                switch (currentTrajectory)
                {
                    case ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory:
                        currentTransition = currentDistance / maxDistance;
                        float parabolicHeight = 4f * shootBulletSo.parabolaHeight * currentTransition * ( 1f - currentTransition);
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
                    Vector3 lookDir = currentPosition - transform.position;
                    if (lookDir.sqrMagnitude >= Mathf.Epsilon) {
                        transform.rotation = Quaternion.LookRotation(lookDir);
                    }
                }

                RaycastHit? hitTest;
                hitTest = TryGetHitInfo(transform.position, currentPosition);
                if (hitTest.HasValue) {
                    SelfDestroy(hitTest);
                    yield break;
                }

                transform.position = currentPosition;
                currentDistance += shootBulletSo.bulletSpeed * Time.deltaTime;

                yield return null;
            }

            SelfDestroy( null );
        }
        
        
        private void HitEffect(Vector3 point)
        {
            if (shootBulletSo.hitEffectPrefab) {
                Destroy(
                    Instantiate(shootBulletSo.hitEffectPrefab, point, Quaternion.identity), shootBulletSo.hitEffectDestroyTime
                );
            }
        }
        
        private void SelfDestroy(RaycastHit? hitInfo)
        {
            if (hitInfo.HasValue)
            {
                HitEffect(hitInfo.Value.point);
                
                if (hitInfo.Value.collider.TryGetComponent(out IDamage damage))
                {
                    damage.DoDamage(hitInfo.Value.collider, shootBulletSo.hitDamageVolume, shootBulletSo.hitDamageForce);
                }
            }
            
            Destroy(gameObject);
        }
        
        private RaycastHit? TryGetHitInfo(Vector3 fromPosition, Vector3 toPosition)
        {
            Ray castRay = new Ray( fromPosition, toPosition - fromPosition );

            if (Physics.SphereCast(
                    castRay, 
                    shootBulletSo.bulletHitRadius, 
                    out RaycastHit hitInfo
                    ,( fromPosition - toPosition ).magnitude
                    ,shootBulletSo.hitLayerMask
                    )
                ) {
                return hitInfo;
            }
            
            return null;
        }
    }
}