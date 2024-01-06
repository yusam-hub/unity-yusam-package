using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damage : MonoBehaviour, IDamage
    {
        [SerializeField] private DamageSo damageSo;

        private IHealth _health;

        private IDamage _parentDamage;

        public void SetParentDamage(IDamage parentDamage)
        {
            _parentDamage = parentDamage;
        }
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        public void DoDamage(Collider collider, float volume, float force)
        {
            _health.MinusHealth(volume);

            if (_health.GetHealth() == 0)
            {
                if (_parentDamage == null)
                {
                    SelfDestroy(collider, force);
                }
            }
        }

        private void SelfDestroy(Collider collider, float force)
        {
            if (damageSo.hitEffectPrefab)
            {
                Destroy(
                    Instantiate(damageSo.hitEffectPrefab, transform.position, Quaternion.identity),
                    damageSo.hitEffectDestroyTime
                );
            }

            Destroy(gameObject);
        }
    }
}