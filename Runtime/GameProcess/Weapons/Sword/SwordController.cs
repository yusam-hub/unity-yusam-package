using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class SwordController : MonoBehaviour
    {
        [SerializeField] private SwordSo swordSo;
        [SerializeField] private Transform swordRayCastStartPoint;
        [SerializeField] private Transform swordRayCastEndPoint;
        
        private DebugProperties _debugProperties;
        private bool _weaponActionInProcess;
        
        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }
        
        public void SetSwordSo(SwordSo newSwordSo)
        {
            swordSo = newSwordSo;
        }

        public void SwordAction()
        {
            if (!_weaponActionInProcess)
            {
                _weaponActionInProcess = true;
                StartCoroutine(ExecuteCoroutine());
            }
        }
        
        private void StartEffect()
        {
            if (swordSo.startEffectPrefab) {
                Destroy(Instantiate(swordSo.startEffectPrefab, swordRayCastStartPoint.position, swordRayCastStartPoint.rotation), swordSo.startEffectDestroyTime);
            }
        }
        
        private void HitEffect(Vector3 point)
        {
            if (swordSo.hitEffectPrefab) {
                Destroy(Instantiate(swordSo.hitEffectPrefab, point, Quaternion.identity), swordSo.hitEffectDestroyTime);
            }
        }
        
        private IEnumerator ExecuteCoroutine()
        {
            StartEffect();
            
            var timer = swordSo.hitDamageActiveLifeTime;
            List<RaycastHit> list = new List<RaycastHit>();
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                var dir = swordRayCastEndPoint.position - swordRayCastStartPoint.position;

                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawLine(swordRayCastStartPoint.position, swordRayCastEndPoint.position, _debugProperties.debugLongLineColor, _debugProperties.debugLongDuration);
                }
                
                var hits = Physics.RaycastAll(swordRayCastStartPoint.position, dir, dir.magnitude, swordSo.hitDamageLayerMask);
                foreach (var hit in hits)
                {
                    if (hit.collider && hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        if (_debugProperties.debugEnabled)
                        {
                            Debug.Log($"Sword hit found [ Damageable :  {damageable} ]");
                        }
                        HitEffect(hit.point);
                        damageable.TakeDamageWithPause(swordSo.hitDamageVolume); 
                    }
                }
                
                yield return null;
            }
            
            StartCoroutine(ExecuteReloadCoroutine());
        }

        private IEnumerator ExecuteReloadCoroutine()
        {
            yield return new WaitForSeconds(swordSo.hitDamageReloadLifeTime);
            
            _weaponActionInProcess = false;
        }
    }
}