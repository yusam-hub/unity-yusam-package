﻿using System;
using UnityEngine;

namespace YusamPackage
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        [SerializeField] private FloatUnityEvent onProgressEvent = new();
        
        public event EventHandler<ProgressFloatEventArgs> OnProgressHealth;

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
            //Debug.Log($"{GetType()}:{name}:ResetHealth");
            
            _healthVolume = healthSo.maxHealth;
            
            DoUpdateProgress();
        }
        
        public void PlusHealth(float volume)
        {
            if (_parentHealth != null)
            {
                _parentHealth.PlusHealth(volume);
                return;
            }
            
            //Debug.Log($"{GetType()}:PlusHealth({volume})");
            
            _healthVolume += volume;
            
            if (_healthVolume > healthSo.maxHealth)
            {
                _healthVolume = healthSo.maxHealth;
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
            
            //Debug.Log($"{GetType()}:MinusHealth({volume})");
            
            _healthVolume -= volume;
            
            if (_healthVolume < 0)
            {
                _healthVolume = 0;
            }
            
            DoUpdateProgress();
        }
    }
}