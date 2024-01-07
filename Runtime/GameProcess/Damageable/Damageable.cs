using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damageable : MonoBehaviour, IDamageable
    {
        private IHealth _health;
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        public void TakeDamage(float volume)
        {
            _health.MinusHealth(volume);
        }
        
        public void TakeDamage(float volume, Collider hitCollider)
        {
            _health.MinusHealth(volume);
        }
        
        public void TakeDamage(float volume, Collider hitCollider, float force)
        {
            _health.MinusHealth(volume);
        }
    }
}