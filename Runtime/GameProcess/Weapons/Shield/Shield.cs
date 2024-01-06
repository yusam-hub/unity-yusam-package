﻿using System;
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

        [HideInInspector]
        public Damage shieldDamage;
        [HideInInspector]
        public Health shieldHealth;
        
        private bool _shieldIsSelfDestroying;
        private bool _shieldInProgress;
        private float _shieldActiveProgress;

        private void Awake()
        {
            shieldDamage = GetComponent<Damage>();
            shieldDamage.doNotSelfDestroy = true;
            shieldDamage.OnSelfDestroy += ShieldDamageOnOnSelfDestroy;
            
            shieldHealth = GetComponent<Health>();
        }

        private void OnDestroy()
        {
            shieldDamage.OnSelfDestroy -= ShieldDamageOnOnSelfDestroy;
        }

        private void ShieldDamageOnOnSelfDestroy(object sender, EventArgs e)
        {
            Debug.Log("ShieldDamageOnOnSelfDestroy");
            _shieldIsSelfDestroying = true;
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
            
            OnShowShield?.Invoke(this, new ProgressFloatEventArgs
            {
                Progress = _shieldActiveProgress,
            });

            var activeLifeTime = shieldSo.activeLifeTime;
            
            var newGameObject = ShieldPrefabCreate(sourceTransform);
            
            while (activeLifeTime > 0)
            {
                if (_shieldIsSelfDestroying)
                {
                    ShieldPrefabDestroy(newGameObject);
                    yield break;
                }
                
                activeLifeTime -= Time.deltaTime;
                
                _shieldActiveProgress = 1f - activeLifeTime / shieldSo.activeLifeTime;
                
                OnProgressShield?.Invoke(this, new ProgressFloatEventArgs
                {
                    Progress = _shieldActiveProgress
                });
                
                yield return null;
            }

            ShieldPrefabDestroy(newGameObject);
        }

        private GameObject ShieldPrefabCreate(Transform sourceTransform)
        {
           return Instantiate(prefabToBeSpawn, sourceTransform);
        }
        
        private void ShieldPrefabDestroy(GameObject prefabGameObject)
        {
            Debug.Log($"ShieldPrefabDestroy");
            
            _shieldActiveProgress = 0;
            _shieldInProgress = false;
            _shieldIsSelfDestroying = false;

            OnHideShield?.Invoke(this, new ProgressFloatEventArgs
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