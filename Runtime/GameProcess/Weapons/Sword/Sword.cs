using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class Sword : MonoBehaviour, IWeaponAction, ISword
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
        
        private IEnumerator ExecuteCoroutine(Transform sourceTransform)
        {

            float timer = swordSo.hitDamageDuration;
            List<Collider> list = new List<Collider>();
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                Vector3 dir = endPoint.position - startPoint.position;
                RaycastHit[] hits = Physics.RaycastAll(startPoint.position, dir, dir.magnitude, layerMask);
                foreach (RaycastHit hit in hits)
                {
                    if (list.IndexOf(hit.collider) < 0)
                    {
                        list.Add(hit.collider);
                    } 
                }
                
                yield return null;
            }
            
            foreach (Collider collider in list)
            {

                if (collider != null)
                {
                    collider.GetComponent<IDamage>()
                        ?.DoDamage(collider, swordSo.hitDamageVolume, swordSo.hitDamageForce);
                }
            }
            
            _weaponActionInProcess = false;
        }
    }
}