using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private DamageableSo damageableSo;
        
        private IHealth _health;
        
        private void Awake()
        {
            if (damageableSo)
            {
                gameObject.layer = LayerMask.NameToLayer(damageableSo.layerName);
                Debug.Log($"Assign layer name [ {damageableSo.layerName} ] to [ {name} ] from Scriptable Object [ {damageableSo.name} ]");
            }

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