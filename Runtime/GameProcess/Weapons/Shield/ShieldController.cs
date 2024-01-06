using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private Shield prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;
        
        [Space(10)]
        
        [SerializeField] private FloatUnityEvent onShowShieldEvent = new();
        [SerializeField] private FloatUnityEvent onProgressShieldEvent = new();
        [SerializeField] private FloatUnityEvent onHideShieldEvent = new();
        
        
        private GameInputController _gameInputController;
        private Shield _shield;


        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(prefabToBeSpawn == null,typeof(Shield).ToString(), this);
            
            _gameInputController = GetComponent<GameInputController>();

            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
            
            _shield = Instantiate(prefabToBeSpawn, transform);
            _shield.OnShowShield += ShieldOnOnShowShield;
            _shield.OnProgressShield += ShieldOnOnProgressShield;
            _shield.OnHideShield += ShieldOnOnHideShield;
        }

        private void ShieldOnOnShowShield(object sender, Shield.OnFloatEventArgs e)
        {
            Debug.Log($"ShieldOnOnShowShield {e.Value}");
            onShowShieldEvent?.Invoke(e.Value);
        }
        
        private void ShieldOnOnProgressShield(object sender, Shield.OnFloatEventArgs e)
        {
            Debug.Log($"ShieldOnOnProgressShield {e.Value}");
            onProgressShieldEvent?.Invoke(e.Value);
        }

        private void ShieldOnOnHideShield(object sender, Shield.OnFloatEventArgs e)
        {
            Debug.Log($"ShieldOnOnHideShield {e.Value}");
            onHideShieldEvent?.Invoke(e.Value);
        }
        
        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;

            _shield.ShieldActivate(transform);
        }


        private void OnDestroy()
        {
            _shield.OnShowShield -= ShieldOnOnShowShield;
            _shield.OnProgressShield -= ShieldOnOnProgressShield;
            _shield.OnHideShield -= ShieldOnOnHideShield;
            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}