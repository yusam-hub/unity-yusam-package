using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Destroyable : MonoBehaviour, IDestroyable
    {
        [SerializeField] private bool selfDestroyOnHealthZero = true;
        
        private Health _health;
        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.OnZeroHealth += HealthOnZeroHealth;
        }

        private void HealthOnZeroHealth(object sender, EventArgs e)
        {
            Debug.Log("HealthOnZeroHealth");
            if (selfDestroyOnHealthZero)
            {
                SelfDestroy();
            }
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}