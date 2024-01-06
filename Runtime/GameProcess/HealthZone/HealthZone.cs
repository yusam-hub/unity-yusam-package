using UnityEngine;

namespace YusamPackage
{
    public class HealthZone : MonoBehaviour, IHealthZone
    {
        [SerializeField] private HealthZoneSo healthZoneSo;

        private Collider _collider;
        private float _damageReUseTimer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            //Debug.Log($"OnTriggerStay");

            _damageReUseTimer -= Time.deltaTime;
            
            if (_damageReUseTimer <= 0)
            {
                _damageReUseTimer = healthZoneSo.healthReUseInterval;
                
                IHealthly[] healthlies = other.GetComponents<IHealthly>();
                foreach (var healthly in healthlies)
                {
                    healthly.TakeHealth(healthZoneSo.healthVolume);
                }   
            }
        }
    }
}