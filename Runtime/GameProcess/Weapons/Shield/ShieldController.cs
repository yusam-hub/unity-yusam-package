﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [DisallowMultipleComponent]
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private Shield prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;
        
        private GameInputController _gameInputController;
        private IWeaponAction _weaponAction;


        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(prefabToBeSpawn == null,typeof(Shield).ToString(), this);
            
            _gameInputController = GetComponent<GameInputController>();

            foreach(var gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
            _weaponAction = Instantiate(prefabToBeSpawn, transform);
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;

            _weaponAction.WeaponAction(transform);
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