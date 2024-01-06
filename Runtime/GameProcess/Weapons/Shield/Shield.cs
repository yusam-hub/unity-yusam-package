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
        public GameObject prefabToBeSpawn;
             
        public event EventHandler<ProgressFloatEventArgs> OnShowShield;
        public event EventHandler<ProgressFloatEventArgs> OnProgressShield;
        public event EventHandler<ProgressFloatEventArgs> OnHideShield;

        public Damage shieldDamage;
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
                StartCoroutine(ExecuteCoroutine(sourceTransform));
            }
        }

        public float GetShieldActiveProgress()
        {
            return _shieldActiveProgress;
        }

        private IEnumerator ExecuteCoroutine(Transform sourceTransform)
        {
            _shieldActiveProgress = 0;
            OnShowShield?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress,
            });

            _shieldInProgress = true;
            
            var activeLifeTime = shieldSo.activeLifeTime;
            
            var newGameObject = Instantiate(prefabToBeSpawn, sourceTransform);
            
            while (activeLifeTime > 0)
            {
                activeLifeTime -= Time.deltaTime;
                
                _shieldActiveProgress = 1f - activeLifeTime / shieldSo.activeLifeTime;
                
                OnProgressShield?.Invoke(this, new ProgressFloatEventArgs
                {
                    Progress = _shieldActiveProgress
                });
                
                yield return null;
            }
            
            Destroy(newGameObject);
            
            _shieldActiveProgress = 0;
            OnHideShield?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress
            });
            
            _shieldInProgress = false;
        }

        
        
        private void Update()
        {
            //OverlapSphere будет использоваться при разрушение шита чтобы собрать все объекты и их уничтожить типа
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 0);
        }
        
    }
}