using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        [SerializeField] private HealthBarSo healthBarSo;
        [SerializeField] private bool healthBarEnabled = false;
        
        [SerializeField] private FloatUnityEvent onProgressEvent = new();
        [SerializeField] private EmptyUnityEvent onZeroHealth = new();
        [SerializeField] private EmptyUnityEvent onMaxHealth = new();
        
        public event EventHandler<ProgressFloatEventArgs> OnProgressHealth;
        public event EventHandler<EventArgs> OnZeroHealth;
        public event EventHandler<EventArgs> OnMaxHealth;
        
        private float _healthVolume;
        private float _healthProgress;
        private IHealth _parentHealth;
        private HealthBarUi _healthBarUi;

        public void SetHealthSo(HealthSo newHealthSo)
        {
            healthSo = newHealthSo;
        }

        public void SetParentHealth(IHealth parentHealth)
        {
            _parentHealth = parentHealth;
        }
        
        private void Awake()
        {
            if (healthBarEnabled && healthBarSo)
            {
                _healthBarUi = Instantiate(healthBarSo.prefab, transform);
                _healthBarUi.SetHealthBarSo(healthBarSo);
            }
        }

        private void Start()
        {
            ResetHealth();
        }

        private void DoUpdateProgress()
        {
            _healthProgress = _healthVolume / healthSo.maxHealth;

            if (_healthBarUi)
            {
                _healthBarUi.SetProgress(_healthProgress);
            }
            
            OnProgressHealth?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _healthProgress,
            });
            
            onProgressEvent?.Invoke(_healthProgress);
        }

        public float GetHealthMax()
        {
            return healthSo.maxHealth;
        }
        
        public bool IsHealthZero()
        {
            return Mathf.RoundToInt(_healthVolume) == 0;
        }

        public bool IsHealthMax()
        {
            return Mathf.RoundToInt(_healthVolume) == Mathf.RoundToInt(healthSo.maxHealth);
        }
        
        public float GetHealth()
        {
            if (_parentHealth != null)
            {
                return _parentHealth.GetHealth();
            }
            return _healthVolume;
        }

        public void ResetHealth()
        {
            if (_parentHealth != null)
            {
                _parentHealth.ResetHealth();
                return;
            }
            
            _healthVolume = healthSo.maxHealth;
            OnMaxHealth?.Invoke(this, EventArgs.Empty);
            onMaxHealth?.Invoke(EventArgs.Empty);
            
            DoUpdateProgress();
        }
        
        public void PlusHealth(float volume)
        {
            if (_parentHealth != null)
            {
                _parentHealth.PlusHealth(volume);
                return;
            }
            
            if (_healthVolume < healthSo.maxHealth)
            {
                _healthVolume += volume;
            }

            if (_healthVolume >= healthSo.maxHealth)
            {
                _healthVolume = healthSo.maxHealth;
                OnMaxHealth?.Invoke(this, EventArgs.Empty);
                onMaxHealth?.Invoke(EventArgs.Empty);
            }

            DoUpdateProgress();
        }

        public void MinusHealth(float volume)
        {
            if (_parentHealth != null)
            {
                _parentHealth.MinusHealth(volume);
                return;
            }

            if (_healthVolume > 0)
            {
                _healthVolume -= volume;
            }

            if (_healthVolume <= 0)
            {
                _healthVolume = 0;
                OnZeroHealth?.Invoke(this, EventArgs.Empty);
                onZeroHealth?.Invoke(EventArgs.Empty);
            }
            
            DoUpdateProgress();
        }
    }
}