using System;
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
        [SerializeField] private Shield prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;
        
        [Space(10)]
        
        [SerializeField] private FloatUnityEvent onShieldShowEvent = new();
        [SerializeField] private FloatUnityEvent onShieldProgressEvent = new();
        [SerializeField] private FloatUnityEvent onShieldHideEvent = new();
        
        [SerializeField] private FloatUnityEvent onHealthProgressEvent = new();
        
        private GameInputController _gameInputController;
        private Shield _shield;
        private Health _ownerHealth;
        private DebugProperties _debugProperties;

        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(prefabToBeSpawn == null,typeof(Shield).ToString(), this);
            
            _gameInputController = GetComponent<GameInputController>();
            _ownerHealth = GetComponent<Health>();
            _debugProperties = GetComponent<DebugProperties>();
            
            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
            
            _shield = Instantiate(prefabToBeSpawn, transform);
            _shield.SetDebugProperties(_debugProperties);
            _shield.OnShieldShow += ShieldOnOnShowShield;
            _shield.OnShieldProgress += ShieldOnOnProgressShield;
            _shield.OnShieldHide += ShieldOnOnHideShield;

            _shield.shieldHealth.OnProgressHealth += ShieldHealthOnOnProgressHealth;
            
        }

        private void ShieldHealthOnOnProgressHealth(object sender, ProgressFloatEventArgs e)
        {
            onHealthProgressEvent?.Invoke(e.Progress);
        }

        private void ShieldOnOnShowShield(object sender, ProgressFloatEventArgs e)
        {
            _ownerHealth.SetParentHealth(_shield.shieldHealth);//устанавливаем шит в качестве здоровья

            onShieldShowEvent?.Invoke(e.Progress);
        }
        
        private void ShieldOnOnProgressShield(object sender, ProgressFloatEventArgs e)
        {
            onShieldProgressEvent?.Invoke(e.Progress);
        }

        private void ShieldOnOnHideShield(object sender, ProgressFloatEventArgs e)
        {
            onShieldHideEvent?.Invoke(e.Progress);
            
            _ownerHealth.SetParentHealth(null);//убираем шит в качестве здоровья
        }
        
        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;
    
            _shield.ShieldActivate(transform);
        }

        private void OnDestroy()
        {
            _shield.shieldHealth.OnProgressHealth -= ShieldHealthOnOnProgressHealth;
            
            _shield.OnShieldShow -= ShieldOnOnShowShield;
            _shield.OnShieldProgress -= ShieldOnOnProgressShield;
            _shield.OnShieldHide -= ShieldOnOnHideShield;
            
            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}