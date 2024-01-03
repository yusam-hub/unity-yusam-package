using System;
using UnityEngine;

namespace YusamPackage
{
    public class DeathZone : MonoBehaviour, IDeathZone
    {
        [SerializeField] private DeathZoneSo deathZoneSo;

        private Collider _collider;
        private float _damageReUseTimer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"OnTriggerEnter");
        }
        
        private void OnTriggerExit(Collider other)
        {
            //Debug.Log($"OnTriggerExit");       
        }

        private void OnTriggerStay(Collider other)
        {
            //Debug.Log($"OnTriggerStay");

            _damageReUseTimer -= Time.deltaTime;
            
            if (_damageReUseTimer <= 0)
            {
                _damageReUseTimer = deathZoneSo.damageReUseInterval;
                
                IDamage[] damages = other.GetComponents<IDamage>();
                foreach (IDamage damage in damages)
                {
                    damage.DoDamage(_collider, deathZoneSo.damage, deathZoneSo.force);
                }   
            }
        }
    }
}