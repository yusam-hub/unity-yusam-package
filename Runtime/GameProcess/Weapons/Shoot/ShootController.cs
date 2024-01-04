﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [RequireComponent(typeof(RotationToMousePointByRay))]
    [RequireComponent(typeof(YusamDebugProperties))]
    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Transform nozzlePoint;
        [SerializeField] private ShootBullet prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;

        private GameInputController _gameInputController;
        private RotationToMousePointByRay _rotationToMousePointByRay;
        private YusamDebugProperties _debugProperties;
        
        private void Awake()
        {
            if (nozzlePoint == null)
            {
                Debug.LogError($"Nozzle Point property is null in component {name}");
            }
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
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.CanUseGameInput()) return;
            
            IWeaponActionToPoint weaponActionToPoint = Instantiate(prefabToBeSpawn, nozzlePoint.position, nozzlePoint.rotation);
            weaponActionToPoint.WeaponActionToPoint(nozzlePoint, _rotationToMousePointByRay.GetMouseLookPosition());

            if (_debugProperties.debugEnabled)
            {
                Debug.DrawLine(nozzlePoint.position, _rotationToMousePointByRay.GetMouseLookPosition(), _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
            }
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