using UnityEngine;

namespace YusamPackage
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthSo healthSo;
        
        private float _healthVolume;
        private HasProgress _hasProgress; 
        
        private void Awake()
        {
            if (healthSo == null)
            {
                //Debug.LogError("Health So prefab not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            
            if (TryGetComponent(out HasProgress hasProgress))
            {
                _hasProgress = hasProgress;
            }
            
            _healthVolume = healthSo.maxHealth;

            //Debug.Log($"Start health {_healthVolume}");
        }

        private void Start()
        {
            DoUpdateProgress();
        }

        private void DoUpdateProgress()
        {
            if (_hasProgress)
            {
                _hasProgress.DoProgressChanged(_healthVolume/healthSo.maxHealth);
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
            
            //Debug.Log($"{name} plus health {volume} and current health became {_healthVolume} of max {healthSo.maxHealth}");  
        }

        public void MinusHealth(float volume)
        {
            _healthVolume -= volume;
            
            if (_healthVolume < 0)
            {
                _healthVolume = 0;
            }
            
            DoUpdateProgress();

            //Debug.Log($"{name} minus health {volume} and current health became {_healthVolume} of max {healthSo.maxHealth}");    
        }
    }
}