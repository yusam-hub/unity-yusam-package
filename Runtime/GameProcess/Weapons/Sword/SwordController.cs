using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class SwordController : MonoBehaviour
    {
        [SerializeField] private SwordSo swordSo;
        [SerializeField] private Transform swordRayCastStartPoint;
        [SerializeField] private Transform swordRayCastEndPoint;
        
        [SerializeField] private GameInputPerformedEnum[] inputs;

        private GameInputController _gameInputController;
        private DebugProperties _debugProperties;
        private bool _weaponActionInProcess;
        
        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
            _gameInputController = GetComponent<GameInputController>();

            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }

        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;
            
            if (!_weaponActionInProcess)
            {
                _weaponActionInProcess = true;
                StartCoroutine(ExecuteCoroutine());
            }
        }

        
        private void StartEffect()
        {
            if (swordSo.startEffectPrefab) {
                Destroy(Instantiate(swordSo.startEffectPrefab, swordRayCastStartPoint.position, swordRayCastStartPoint.rotation), swordSo.startEffectDestroyTime);
            }
        }
        
        private void HitEffect(Vector3 point)
        {
            if (swordSo.hitEffectPrefab) {
                Destroy(Instantiate(swordSo.hitEffectPrefab, point, Quaternion.identity), swordSo.hitEffectDestroyTime);
            }
        }
        
        private IEnumerator ExecuteCoroutine()
        {
            StartEffect();
            
            var timer = swordSo.hitDamageActiveLifeTime;
            List<RaycastHit> list = new List<RaycastHit>();
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                var dir = swordRayCastEndPoint.position - swordRayCastStartPoint.position;

                if (_debugProperties.debugEnabled)
                {
                    Debug.DrawLine(swordRayCastStartPoint.position, swordRayCastEndPoint.position, _debugProperties.debugLongLineColor, _debugProperties.debugLongDuration);
                }
                
                var hits = Physics.RaycastAll(swordRayCastStartPoint.position, dir, dir.magnitude, swordSo.hitDamageLayerMask);
                foreach (var hit in hits)
                {
                    if (list.IndexOf(hit) < 0)
                    {
                        list.Add(hit);
                    } 
                }
                
                yield return null;
            }
            
            if (_debugProperties.debugEnabled)
            {
                Debug.Log($"Sword hit found [ {list.Count} ] colliders");
            }
            
            List<IDamageable> damagablelist = new List<IDamageable>();
            foreach (var raycastHit in list)
            {
                if (raycastHit.collider && raycastHit.collider.TryGetComponent(out IDamageable damageable))
                {
                    if (damagablelist.IndexOf(damageable) < 0)
                    {
                        HitEffect(raycastHit.point);
                        damagablelist.Add(damageable);
                    }
                }
            }
            
            foreach (var damageable in damagablelist)
            {
                damageable.TakeDamage(swordSo.hitDamageVolume); 
            }

            StartCoroutine(ExecuteReloadCoroutine());
        }

        private IEnumerator ExecuteReloadCoroutine()
        {
            yield return new WaitForSeconds(swordSo.hitDamageReloadLifeTime);
            
            _weaponActionInProcess = false;
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