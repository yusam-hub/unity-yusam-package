using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damage : MonoBehaviour, IDamage
    {
        [SerializeField] private DamageSo damageSo;

        public event EventHandler<EventArgs> OnSelfDestroy;

        [HideInInspector]
        public bool doNotSelfDestroy;
            
        private IHealth _health;
        private IDamage _parentDamage;
        private bool _selfDestroying;

        public void SetParentDamage(IDamage parentDamage)
        {
            _parentDamage = parentDamage;
        }
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        public void DoDamage(Collider hitCollider, float volume, float force)
        {
            if (!_selfDestroying)
            {
                _health.MinusHealth(volume);
            }

            if (_health.GetHealth() == 0 && !_selfDestroying)
            {
                _selfDestroying = true;
                SelfDestroy(hitCollider, force);
            }
        }

        public void SelfDestroy(Collider hitCollider, float force)
        {
            if (_parentDamage != null)
            {
                _parentDamage.SelfDestroy(hitCollider, force);
                return;
            }
            
            OnSelfDestroy?.Invoke(this, EventArgs.Empty);
            
            if (damageSo.hitEffectPrefab && hitCollider)
            {
                Destroy(
                    Instantiate(damageSo.hitEffectPrefab, transform.position, Quaternion.identity),
                    damageSo.hitEffectDestroyTime
                );
            }

            if (!doNotSelfDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}