using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    public class Sword : MonoBehaviour
    {
        [SerializeField] private SwordSo swordSo;
        
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LayerMask layerMask;

        private YusamDebugProperties _debugProperties;
        private bool _attackInProcess;

        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DoAttack();
            }
        }

        private void DoAttack()
        {
            if (!_attackInProcess)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
        
        private IEnumerator AttackCoroutine()
        {
            _attackInProcess = true;
            
            float timer = swordSo.hitDamageDuration;
            List<Collider> list = new List<Collider>();
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                Vector3 dir = endPoint.position - startPoint.position;
                RaycastHit[] hits = Physics.RaycastAll(startPoint.position, dir, swordSo.rayCastMaxDistance, layerMask);
                foreach (RaycastHit hit in hits)
                {
                    if (list.IndexOf(hit.collider) < 0)
                    {
                        list.Add(hit.collider);
                    } 
                }

                if (_debugProperties.enabled)
                {
                    Debug.DrawRay(startPoint.position, dir, _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
                }
                
                yield return null;
            }
            
            foreach (Collider collider in list)
            {
                Debug.Log($"{collider.name}");
                collider.GetComponent<IDamage>()?.DoDamage(collider, swordSo.hitDamageVolume, swordSo.hitDamageForce);
            }
            
            _attackInProcess = false;
        }
    }
}