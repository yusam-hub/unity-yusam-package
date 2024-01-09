using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private DamageableSo damageableSo;
        
        private IHealth _health;
        private bool _isPausing;
        private float _pauseTimer;
        
        private void Awake()
        {
            if (damageableSo)
            {
                if (damageableSo.replaceLayer)
                {
                    gameObject.layer = LayerMask.NameToLayer(damageableSo.layerName);
                    Debug.Log(
                        $"Assign layer name [ {damageableSo.layerName} ] to [ {name} ] from Scriptable Object [ {damageableSo.name} ]");
                }
            }

            _health = GetComponent<IHealth>();
        }

        private void Update()
        {
            if (_isPausing)
            {
                _pauseTimer -= Time.deltaTime;
                if (_pauseTimer < 0)
                {
                    _pauseTimer = 0;
                    _isPausing = false;
                }
            }
        }

        public void TakeDamage(float volume)
        {
            _health.MinusHealth(volume);
        }
        
        public void TakeDamageWithPause(float volume)
        {
            if (!_isPausing)
            {
                _isPausing = true;
                _pauseTimer = damageableSo.pauseTakeDamage;
                
                _health.MinusHealth(volume);
            }
        }
    }
}