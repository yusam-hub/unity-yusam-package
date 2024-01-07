using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    [RequireComponent(typeof(GameInputController))]
    [RequireComponent(typeof(Health))]
    [DisallowMultipleComponent]
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ShieldSo shieldSo;
        [SerializeField] private GameInputPerformedEnum[] inputs;
        
        [SerializeField] private FloatUnityEvent onShieldProgressEvent = new();
        [SerializeField] private FloatUnityEvent onShieldReloadProgressEvent = new();
        
        private GameInputController _gameInputController;
        private DebugProperties _debugProperties;
        private GameObject _shield;
        private Health _ownerHealth;
        private Health _shieldHealth;
        private bool _shieldInProgress;
        private bool _shieldIsZeroHealth;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            _gameInputController = GetComponent<GameInputController>();
            _ownerHealth = GetComponent<Health>();
            
            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;
    
            ShieldActivate();
        }
        
        private void ShieldActivate()
        {
            if (!_shieldInProgress)
            {
                _shieldInProgress = true;
                _shieldIsZeroHealth = false;

                _shield = Instantiate(shieldSo.prefabOnActivateShield, transform);
                _shieldHealth = _shield.GetComponent<Health>();
                _shieldHealth.OnZeroHealth += ShieldHealthOnOnZeroHealth;
                _ownerHealth.SetParentHealth(_shieldHealth);
                
                StartCoroutine(ShieldActivateCoroutine());
            }
        }

        private void ShieldHealthOnOnZeroHealth(object sender, EventArgs e)
        {
            _shieldIsZeroHealth = true;
        }

        private IEnumerator ShieldActivateCoroutine()
        {
            var shieldActiveLifeTime = shieldSo.shieldActiveLifeTime;

            while (shieldActiveLifeTime > 0)
            {
                shieldActiveLifeTime -= Time.deltaTime;

                if (_shieldIsZeroHealth)
                {
                    ShieldPrefabDestroy(true);
                    yield break;
                }

                onShieldProgressEvent?.Invoke(1f - shieldActiveLifeTime / shieldSo.shieldActiveLifeTime);

                yield return null;
            }
            
            ShieldPrefabDestroy();
        }
        
        private IEnumerator ShieldReloadCoroutine()
        {
            var shieldReloadLifeTime = shieldSo.shieldReloadLifeTime;
            
            while (shieldReloadLifeTime > 0)
            {
                shieldReloadLifeTime -= Time.deltaTime;
                
                onShieldReloadProgressEvent?.Invoke(1f - shieldReloadLifeTime / shieldSo.shieldReloadLifeTime);
                
                yield return null;
            }
            
            onShieldProgressEvent?.Invoke(0);
            onShieldReloadProgressEvent?.Invoke(0);

            _shieldHealth.OnZeroHealth -= ShieldHealthOnOnZeroHealth;
            _shieldHealth = null;
            Destroy(_shield);
            
            _shieldInProgress = false;
        }
        
        private void ShieldPrefabDestroy(bool withHealthZero = false)
        {
            _ownerHealth.SetParentHealth(null);
            _shield.SetActive(false);
            
            StartCoroutine(ShieldReloadCoroutine());

            if (withHealthZero)
            {
                if (shieldSo.prefabOnDestroyShield)
                {
                    Destroy(Instantiate(shieldSo.prefabOnDestroyShield, transform.position, Quaternion.identity), shieldSo.lifeTimeOnDestroyShield);
                }

                TakeDamageForAllDamageable();
            }
        }
        
        private void TakeDamageForAllDamageable()
        {
            var startPos = transform.position;
            startPos.y = 0;

            Collider[] colliders = Physics.OverlapSphere(transform.position, shieldSo.radiusOnDestroyShield, shieldSo.layerMaskOnDestroyShield);
            
            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"Found [ {colliders.Length} ] for [ TakeDamage ] by radius [ {shieldSo.radiusOnDestroyShield} ]");
            }
            
            foreach (var foundCollider in colliders)
            {
                if (foundCollider.TryGetComponent(out IDamageable damagable))
                {
                    var endPos = foundCollider.transform.position;
                    if (_debugProperties.debugEnabled)
                    {
                        endPos.y = 1;
                        startPos.y = 1;
                        Debug.DrawLine(startPos, endPos, _debugProperties.debugLongLineColor, _debugProperties.debugLongDuration);
                    }
                    damagable.TakeDamage(shieldSo.damageVolumeOnDestroyShield, foundCollider);
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}