using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    [AddComponentMenu("YusamPackage/Game Process/Destroyable")]
    public class Destroyable : MonoBehaviour, IDestroyable
    {
        [SerializeField] private DestroyableSo destroyableSo;
        [SerializeField] private bool selfDestroyOnHealthZero = true;
        [SerializeField] private EmptyUnityEvent onDestroy;
        [SerializeField] private IntUnityEvent onDestroyWithBonus;
        
        private Health _health;
        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.OnZeroHealth += HealthOnZeroHealth;
        }

        private void HealthOnZeroHealth(object sender, EventArgs e)
        {

            if (selfDestroyOnHealthZero)
            {
                SelfDestroy();
            }
        }

        public void SelfDestroy()
        {
            if (destroyableSo)
            {
                if (destroyableSo.prefabOnSelfDestroy)
                {
                    var rotY = Random.Range(destroyableSo.prefabRotateAngleStartY, destroyableSo.prefabRotateAngleEndY);
                    var rotation = transform.rotation * Quaternion.Euler(0, rotY, 0);
                    Destroy(Instantiate(destroyableSo.prefabOnSelfDestroy, transform.position, rotation), destroyableSo.prefabLifeTime);
                }
                
                onDestroyWithBonus?.Invoke(destroyableSo.destroyBonus);
                Experience.Instance.AddBonus(destroyableSo.destroyBonus);
            }

            onDestroy?.Invoke(EventArgs.Empty);
            
            Destroy(gameObject);
        }
    }
}