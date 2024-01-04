﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [RequireComponent(typeof(RotationToMousePointByRay))]
    [RequireComponent(typeof(YusamDebugProperties))]
    [DisallowMultipleComponent]
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private Shield prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;

        private GameInputController _gameInputController;
        private RotationToMousePointByRay _rotationToMousePointByRay;
        private YusamDebugProperties _debugProperties;
        private IWeaponAction _weaponAction;
        private void Awake()
        {
            if (prefabToBeSpawn == null)
            {
                Debug.LogError($"Prefab To Be Spawn property is null in component {name}");
            }
            
            _gameInputController = GetComponent<GameInputController>();
            _rotationToMousePointByRay = GetComponent<RotationToMousePointByRay>();
            _debugProperties = GetComponent<YusamDebugProperties>();

            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
            _weaponAction = Instantiate(prefabToBeSpawn, transform);
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.CanUseGameInput()) return;

            _weaponAction.WeaponAction(transform);
        }


        private void OnDestroy()
        {
            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
            }
        }
    }
}