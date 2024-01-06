using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class Sword : MonoBehaviour, IWeaponAction
    {
        [SerializeField] private SwordSo swordSo;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LayerMask layerMask;
        
        private bool _weaponActionInProcess;

        public void WeaponAction(Transform sourceTransform)
        {
            if (!_weaponActionInProcess)
            {
                _weaponActionInProcess = true;
                StartCoroutine(ExecuteCoroutine(sourceTransform));
            }
        }
        
        private void StartEffect(Transform sourceTransform)
        {
            if (swordSo.startEffectPrefab) {
                Destroy(
                    Instantiate(swordSo.startEffectPrefab, sourceTransform.transform), swordSo.startEffectDestroyTime
                );
            }
        }
        
        private void HitEffect(Vector3 point)
        {
            if (swordSo.hitEffectPrefab) {
                Destroy(
                    Instantiate(swordSo.hitEffectPrefab, point, Quaternion.identity), swordSo.hitEffectDestroyTime
                );
            }
        }
        
        private IEnumerator ExecuteCoroutine(Transform sourceTransform)
        {
            StartEffect(sourceTransform);
            
            var timer = swordSo.hitDamageDuration;
            List<Collider> list = new List<Collider>();
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                var dir = endPoint.position - startPoint.position;
                var hits = Physics.RaycastAll(startPoint.position, dir, dir.magnitude, layerMask);
                foreach (var hit in hits)
                {
                    if (list.IndexOf(hit.collider) < 0)
                    {
                        list.Add(hit.collider);
                    } 
                }
                
                yield return null;
            }
            
            foreach (var collider in list)
            {
                if (collider)
                {
                    
                    HitEffect(collider.transform.position);
                    
                    collider.GetComponent<IDamageable>()
                        ?.TakeDamage(swordSo.hitDamageVolume, collider, swordSo.hitDamageForce);
                }
            }
            
            _weaponActionInProcess = false;
        }
    }
}