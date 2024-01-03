using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damage : MonoBehaviour, IDamage
    {
        [SerializeField] private DamageSo damageSo;

        private IHealth _health;
            
        private void Awake()
        {
            if (damageSo == null)
            {
                //Debug.LogError("Damage So prefab not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            
            _health = GetComponent<IHealth>();
        }

        public void DoDamage(Collider collider, float volume, float force)
        {
            _health.MinusHealth(volume);

            if (_health.GetHealth() == 0)
            {
                //Debug.Log($"Destroy {name}");
                SelfDestroy(collider, force);
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