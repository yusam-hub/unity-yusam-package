using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace YusamPackage
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Health))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private ShieldSo shieldSo;
        public GameObject shieldPrefabToBeSpawn;
  
        public event EventHandler<ProgressFloatEventArgs> OnShieldShow;
        public event EventHandler<ProgressFloatEventArgs> OnShieldProgress;
        public event EventHandler<ProgressFloatEventArgs> OnShieldHide;

        [FormerlySerializedAs("shieldDamage")] [HideInInspector]
        public Damageable shieldDamageable;
        [HideInInspector]
        public Health shieldHealth;
        
        private bool _shieldInProgress;
        private float _shieldActiveProgress;

        private void Awake()
        {
            shieldDamageable = GetComponent<Damageable>();
            shieldHealth = GetComponent<Health>();
        }


        public void ShieldActivate(Transform sourceTransform)
        {
            if (!_shieldInProgress)
            {
                StartCoroutine(ShieldActivateCoroutine(sourceTransform));
            }
        }

        private IEnumerator ShieldActivateCoroutine(Transform sourceTransform)
        {
            _shieldActiveProgress = 0;
            _shieldInProgress = true;
            
            OnShieldShow?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress,
            });

            var shieldActiveLifeTime = shieldSo.shieldActiveLifeTime;
            
            var newGameObject = ShieldPrefabCreate(sourceTransform);
            
            while (shieldActiveLifeTime > 0)
            {
                shieldActiveLifeTime -= Time.deltaTime;

                if (shieldHealth.GetHealth() <= 0)
                {
                    ShieldPrefabDestroy(newGameObject);
                    yield break;
                }
                
                _shieldActiveProgress = 1f - shieldActiveLifeTime / shieldSo.shieldActiveLifeTime;
                
                OnShieldProgress?.Invoke(this, new ProgressFloatEventArgs
                {
                    Progress = _shieldActiveProgress
                });

                yield return null;
            }

            ShieldPrefabDestroy(newGameObject);
        }
        
        private IEnumerator ShieldReloadCoroutine()
        {
            while (shieldHealth.GetHealth() < shieldHealth.GetHealthMax())
            {
                shieldHealth.PlusHealth(1); //todo: нужно расчитать за какое время нужно восстановить здоровье щита
                
                yield return null;
            }
            
            _shieldInProgress = false;
        }

        private GameObject ShieldPrefabCreate(Transform sourceTransform)
        {
           return Instantiate(shieldPrefabToBeSpawn, sourceTransform);
        }
        
        private void ShieldPrefabDestroy(GameObject prefabGameObject)
        {
            //Debug.Log($"ShieldPrefabDestroy");
            
            _shieldActiveProgress = 0;

            OnShieldHide?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress
            });
            
            Destroy(prefabGameObject);
            
            StartCoroutine(ShieldReloadCoroutine());
        }
        
        private void Update()
        {
            //OverlapSphere будет использоваться при разрушение шита чтобы собрать все объекты и их уничтожить типа
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 0);
        }
        
    }
}