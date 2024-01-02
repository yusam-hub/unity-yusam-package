using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    public class ShootBullet : MonoBehaviour
    {
        public enum ShootBulletTrajectory
        {
            LinerTrajectory,
            ParabolaTrajectory
        }

        [SerializeField] private ShootBulletTrajectory trajectory = ShootBulletTrajectory.LinerTrajectory;
        [SerializeField] private bool turnToTrajectory = true;
        [SerializeField] private float arcHeight = 2;
        [SerializeField] private float speed = 20;
        [SerializeField] private float hitBoxRadius = .2f;
        [SerializeField] private LayerMask hitLayerMask;
        
        private Vector3 _fromPosition;
        private Vector3 _toPosition;
        
        
        public void Shoot(Vector3 fromPosition, Vector3 toPosition)
        {
            _fromPosition = fromPosition;
            _toPosition = toPosition;

            if (trajectory == ShootBulletTrajectory.LinerTrajectory)
            {
                StartCoroutine(LinerCoroutine());
            }
            else
            {
                StartCoroutine(ParabolaCoroutine());
            }
        }

        private IEnumerator LinerCoroutine()
        {
            Vector3 startPos = _fromPosition;
            Vector3 direction = _toPosition - startPos;
            
            float distance = direction.magnitude;

            float currentPath = 0;
            while (currentPath <= distance) {
                float part = currentPath / distance;
                Vector3 pos = Vector3.Lerp( startPos, _toPosition, part );
                
                if (turnToTrajectory) {
                    Vector3 lookDir = pos - transform.position;
                    if (lookDir.sqrMagnitude >= Mathf.Epsilon) {
                        transform.rotation = Quaternion.LookRotation( lookDir );
                    }
                }

                RaycastHit? hitTest;
                hitTest = CheckMainHit( transform.position, pos );
                if (hitTest.HasValue) {
                    SelfDestroy( hitTest );
                    yield break;
                }/* else {
                    hitTest = CheckSpeculativeHit( transform.position, pos );
                    if (hitTest.HasValue) {
                        SelfDestroy( hitTest );
                        yield break;
                    }
                }*/

                transform.position = pos;
                currentPath += speed * Time.deltaTime;
                yield return null;
            }

            SelfDestroy( null );
        }
        
        private IEnumerator ParabolaCoroutine()
        {
            Vector3 startPos = _fromPosition;
            Vector3 direction = _toPosition - startPos;
            direction.y = 0;
            float length = direction.magnitude;

            float currentPath = 0;
            while (currentPath <= length) {
                float part = currentPath / length;
                float parabolicHeight = 4f * arcHeight * part * ( 1f - part );
                Vector3 pos = Vector3.Lerp( startPos, _toPosition, part );
                pos.y += parabolicHeight;
                
                if (turnToTrajectory) {
                    Vector3 lookDir = pos - transform.position;
                    if (lookDir.sqrMagnitude >= Mathf.Epsilon) {
                        transform.rotation = Quaternion.LookRotation( lookDir );
                    }
                }

                RaycastHit? hitTest;
                hitTest = CheckMainHit( transform.position, pos );
                if (hitTest.HasValue) {
                    SelfDestroy( hitTest );
                    yield break;
                }/* else {
                    hitTest = CheckSpeculativeHit( transform.position, pos );
                    if (hitTest.HasValue) {
                        SelfDestroy( hitTest );
                        yield break;
                    }
                }*/

                transform.position = pos;
                currentPath += speed * Time.deltaTime;
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

            if (Physics.SphereCast(castRay, hitBoxRadius, out RaycastHit hitInfo, ( point1 - point2 ).magnitude, hitLayerMask)) {
                return hitInfo;
            } else {
                return null;
            }
        }
        
        /*private RaycastHit? CheckSpeculativeHit(Vector3 point1, Vector3 point2)
        {
            Vector3 groundPoint = transform.position;
            groundPoint.y = 0;
            Vector3 direction = point2 - point1;
            // ЗлоЕбучая магия
            if (Physics.CapsuleCast( transform.position,
                    groundPoint,
                    hitBoxRadius,
                    direction.normalized,
                    out RaycastHit hitInfo,
                    direction.magnitude,
                    GlobalSettings.Instance.SpeculativeHitLayers,
                    QueryTriggerInteraction.Collide )) {
                return hitInfo;
            } else {
                return null;
            }
        }*/
    }
}