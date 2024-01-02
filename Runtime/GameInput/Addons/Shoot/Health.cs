using System;
using UnityEngine;

namespace YusamPackage
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        
        private float _healthVolume;

        private void Awake()
        {
            if (healthSo == null)
            {
                Debug.LogError("Health So prefab not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            
            _healthVolume = healthSo.maxHealth;
            Debug.Log($"Start health {_healthVolume}");
        }

        public float GetHealth()
        {
            return _healthVolume;
        }
        
        public void PlusHealth(float volume)
        {
            _healthVolume += volume;
            if (_healthVolume > healthSo.maxHealth)
            {
                _healthVolume = healthSo.maxHealth;
            }

            Debug.Log($"{name} plus health {volume} and current health became {_healthVolume} of max {healthSo.maxHealth}");  
        }

        public void MinusHealth(float volume)
        {
            _healthVolume -= volume;
            if (_healthVolume < 0)
            {
                _healthVolume = 0;
            }

            Debug.Log($"{name} minus health {volume} and current health became {_healthVolume} of max {healthSo.maxHealth}");    
        }
    }
}