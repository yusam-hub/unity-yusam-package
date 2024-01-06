using System;
using UnityEngine;

namespace YusamPackage
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        [SerializeField] private FloatUnityEvent onProgressEvent = new();
        
        public event EventHandler<ProgressFloatEventArgs> OnProgressHealth;
        public event EventHandler<EventArgs> OnZeroHealth;
        public event EventHandler<EventArgs> OnMaxHealth;
        
        private float _healthVolume;
        private HasProgress _hasProgress; 
        private float _healthProgress;
        
        private IHealth _parentHealth;

        public void SetParentHealth(IHealth parentHealth)
        {
            _parentHealth = parentHealth;
        }
        
        private void Awake()
        {
            if (TryGetComponent(out HasProgress hasProgress))
            {
                _hasProgress = hasProgress;
            }
        }

        private void Start()
        {
            ResetHealth();
        }

        private void DoUpdateProgress()
        {
            _healthProgress = _healthVolume / healthSo.maxHealth;
            
            OnProgressHealth?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _healthProgress,
            });
            
            onProgressEvent?.Invoke(_healthProgress);
            
            if (_hasProgress)
            {
                _hasProgress.DoProgressChanged(_healthProgress);
            }
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

            if (_healthVolume > healthSo.maxHealth)
            {
                _healthVolume = healthSo.maxHealth;
                OnMaxHealth?.Invoke(this, EventArgs.Empty);
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

            if (_healthVolume < 0)
            {
                _healthVolume = 0;
                OnZeroHealth?.Invoke(this, EventArgs.Empty);
            }
            
            DoUpdateProgress();
        }
    }
}