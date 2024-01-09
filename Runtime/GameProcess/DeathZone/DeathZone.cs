using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class DeathZone : MonoBehaviour, IDeathZone
    {
        [SerializeField] private DeathZoneSo deathZoneSo;

        private Collider _collider;
        private float _damageReUseTimer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            if (_collider)
            {
                _collider.isTrigger = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //Debug.Log($"OnTriggerStay");

            _damageReUseTimer -= Time.deltaTime;
            
            if (_damageReUseTimer <= 0)
            {
                _damageReUseTimer = deathZoneSo.damageReUseInterval;
                
                IDamageable[] damages = other.GetComponents<IDamageable>();
                foreach (var damage in damages)
                {
                    damage.TakeDamage(deathZoneSo.damageVolume);
                }   
            }
        }
    }
}