using System;
using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private ShootBulletSo shootBulletSo;

        public void Shoot(Transform fromTransform, Vector3 toPosition)
        {
            StartCoroutine(MoveBulletCoroutine(fromTransform, toPosition, shootBulletSo.trajectory));
        }

        private IEnumerator MoveBulletCoroutine(Transform fromTransform, Vector3 toPosition, ShootBulletSo.ShootBulletTrajectory trajectory)
        {
            Vector3 currentDirection;
            float maxDistance;
            
            Vector3 startPos = fromTransform.position;
            Vector3 endPos = toPosition;
            ShootBulletSo.ShootBulletTrajectory currentTrajectory = trajectory;

            currentDirection = endPos - startPos;
            maxDistance = currentDirection.magnitude;
            
            if (
                shootBulletSo.parabolaMinDistance > 0
                &&
                currentTrajectory == ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory
                &&
                maxDistance < shootBulletSo.parabolaMinDistance
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

            float currentDistance = 0;
            while (currentDistance <= maxDistance) {
                
                float currentTransition;
                Vector3 currentPosition;
                
                switch (currentTrajectory)
                {
                    case ShootBulletSo.ShootBulletTrajectory.ParabolaTrajectory:
                        currentTransition = currentDistance / maxDistance;
                        float parabolicHeight = 4f * shootBulletSo.parabolaHeight * currentTransition * ( 1f - currentTransition );
                        currentPosition = Vector3.Lerp( startPos, endPos, currentTransition );
                        currentPosition.y += parabolicHeight;
                        break;
                    case ShootBulletSo.ShootBulletTrajectory.LinerTrajectory:
                        currentTransition = currentDistance / maxDistance;
                        currentPosition = Vector3.Lerp( startPos, endPos, currentTransition );
                        break;
                    default:
                        currentTransition = currentDistance / maxDistance;
                        currentPosition = Vector3.Lerp( startPos, endPos, currentTransition );
                        break;                        
                }
                
                if (shootBulletSo.turnToTrajectory) {
                    Vector3 lookDir = currentPosition - transform.position;
                    if (lookDir.sqrMagnitude >= Mathf.Epsilon) {
                        transform.rotation = Quaternion.LookRotation( lookDir );
                    }
                }

                RaycastHit? hitTest;
                hitTest = CheckMainHit( transform.position, currentPosition );
                if (hitTest.HasValue) {
                    SelfDestroy( hitTest );
                    yield break;
                }

                transform.position = currentPosition;
                currentDistance += shootBulletSo.bulletSpeed * Time.deltaTime;

                yield return null;
            }

            SelfDestroy( null );
        }
        
        
        private void SelfDestroy(RaycastHit? hitInfo)
        {
            if (hitInfo != null)
            {
                Debug.Log($"Hit info {hitInfo.Value.transform.gameObject.name}");
            }
            /*if (hitInfo.HasValue) 
            {
                if (_missileSetup.hitEffect) {
                    Destroy(Instantiate( _missileSetup.hitEffect, hitInfo.Value.point, Quaternion.identity ), 1f);
                }
            } 
            else 
            {
                if (_missileSetup.hitEffect) 
                {
                    Destroy(Instantiate( _missileSetup.hitEffect, transform.position, Quaternion.identity ), 1f);
                }
            }
            if(hitInfo.HasValue)
            {
                hitInfo.Value.collider.GetComponent<IDamagable>()?.TakeDamage(hitInfo.Value.collider, _missileSetup.damage);
            }
            foreach (var spell in _missileSetup.GetChainedSpells()) {
                CreateSpell.CreateChainedSpell( spell, transform.position, transform.rotation, _fraction );
            }*/

            Destroy(gameObject);
        }
        
        private RaycastHit? CheckMainHit(Vector3 point1, Vector3 point2)
        {
            Ray castRay = new Ray( point1, point2 - point1 );   

            if (Physics.SphereCast(castRay, shootBulletSo.bulletHitRadius, out RaycastHit hitInfo, ( point1 - point2 ).magnitude, shootBulletSo.bulletHitLayerMask)) {
                return hitInfo;
            } else {
                return null;
            }
        }
    }
}