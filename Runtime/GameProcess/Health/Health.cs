using System;
using UnityEngine;

namespace YusamPackage
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        [SerializeField] private FloatUnityEvent onProgressEvent = new();
        
        private float _healthVolume;
        private HasProgress _hasProgress; 
        
        public event EventHandler<ProgressFloatEventArgs> OnProgressHealth;

        private float _healthProgress;
        
        private void Awake()
        {
            if (TryGetComponent(out HasProgress hasProgress))
            {
                _hasProgress = hasProgress;
            }
            
            _healthVolume = healthSo.maxHealth;
        }

        private void Start()
        {
            DoUpdateProgress();
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

            DoUpdateProgress();
        }

        public void MinusHealth(float volume)
        {
            _healthVolume -= volume;
            
            if (_healthVolume < 0)
            {
                _healthVolume = 0;
            }
            
            DoUpdateProgress();
        }
    }
}