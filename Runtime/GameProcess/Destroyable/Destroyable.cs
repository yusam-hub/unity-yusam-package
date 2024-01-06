using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
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
            if (destroyableSo.prefabOnSelfDestroy)
            {
                Destroy(
                    Instantiate(destroyableSo.prefabOnSelfDestroy, transform)
                    , destroyableSo.prefabLifeTime
                    );
            }
            
            onDestroy?.Invoke(EventArgs.Empty);
            onDestroyWithBonus?.Invoke(destroyableSo.destroyBonus);
            
            Destroy(gameObject);
        }
    }
}