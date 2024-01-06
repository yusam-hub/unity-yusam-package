using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YusamPackage
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private ShieldSo shieldSo;
        public GameObject prefabToBeSpawn;
             
        public class OnFloatEventArgs : EventArgs
        {
            public float Value;
        }
        public event EventHandler<OnFloatEventArgs> OnShowShield;
        public event EventHandler<OnFloatEventArgs> OnProgressShield;
        public event EventHandler<OnFloatEventArgs> OnHideShield;

        private bool _shieldInProgress;
        private float _shieldActiveProgress;
        
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
            OnShowShield?.Invoke(this, new OnFloatEventArgs
            {
                Value = _shieldActiveProgress
            });

            _shieldInProgress = true;
            
            var activeLifeTime = shieldSo.activeLifeTime;
            
            var newGameObject = Instantiate(prefabToBeSpawn, sourceTransform);
            
            while (activeLifeTime > 0)
            {
                activeLifeTime -= Time.deltaTime;
                
                _shieldActiveProgress = 1f - activeLifeTime / shieldSo.activeLifeTime;
                
                OnProgressShield?.Invoke(this, new OnFloatEventArgs
                {
                    Value = _shieldActiveProgress
                });
                
                yield return null;
            }
            
            Destroy(newGameObject);
            
            _shieldActiveProgress = 0;
            OnHideShield?.Invoke(this, new OnFloatEventArgs
            {
                Value = _shieldActiveProgress
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