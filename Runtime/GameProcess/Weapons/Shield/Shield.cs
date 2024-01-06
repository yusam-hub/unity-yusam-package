using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YusamPackage
{
    [RequireComponent(typeof(Damage))]
    [RequireComponent(typeof(Health))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private ShieldSo shieldSo;
        public GameObject shieldPrefabToBeSpawn;
  
        public event EventHandler<ProgressFloatEventArgs> OnShieldShow;
        public event EventHandler<ProgressFloatEventArgs> OnShieldProgress;
        public event EventHandler<ProgressFloatEventArgs> OnShieldHide;

        [HideInInspector]
        public Damage shieldDamage;
        [HideInInspector]
        public Health shieldHealth;
        
        private bool _shieldInProgress;
        private float _shieldActiveProgress;

        private void Awake()
        {
            shieldDamage = GetComponent<Damage>();
            shieldHealth = GetComponent<Health>();
        }

        public void ShieldActivate(Transform sourceTransform)
        {
            if (!_shieldInProgress)
            {
                StartCoroutine(ShieldExecuteCoroutine(sourceTransform));
            }
        }

        private IEnumerator ShieldExecuteCoroutine(Transform sourceTransform)
        {
            _shieldActiveProgress = 0;
            _shieldInProgress = true;
            
            OnShieldShow?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress,
            });

            var activeLifeTime = shieldSo.activeLifeTime;
            
            var newGameObject = ShieldPrefabCreate(sourceTransform);
            
            while (activeLifeTime > 0 )
            {
                activeLifeTime -= Time.deltaTime;
                
                _shieldActiveProgress = 1f - activeLifeTime / shieldSo.activeLifeTime;
                
                OnShieldProgress?.Invoke(this, new ProgressFloatEventArgs
                {
                    Progress = _shieldActiveProgress
                });

                yield return null;
            }

            ShieldPrefabDestroy(newGameObject);
        }

        private GameObject ShieldPrefabCreate(Transform sourceTransform)
        {
           return Instantiate(shieldPrefabToBeSpawn, sourceTransform);
        }
        
        private void ShieldPrefabDestroy(GameObject prefabGameObject)
        {
            Debug.Log($"ShieldPrefabDestroy");
            
            _shieldActiveProgress = 0;
            _shieldInProgress = false;

            OnShieldHide?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress
            });
            
            shieldHealth.ResetHealth();
            
            Destroy(prefabGameObject);
        }
        
        private void Update()
        {
            //OverlapSphere будет использоваться при разрушение шита чтобы собрать все объекты и их уничтожить типа
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 0);
        }
        
    }
}