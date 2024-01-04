using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [RequireComponent(typeof(GameInputController))]
    [RequireComponent(typeof(RotationToMousePointByRay))]
    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Transform nozzlePoint;
        [SerializeField] private ShootBullet prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;

        private GameInputController _gameInputController;
        private RotationToMousePointByRay _rotationToMousePointByRay;
        private float _reloadTimer;
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

            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.CanUseGameInput()) return;
            
            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer <= 0)
            {
                ShootBullet shootBullet = Instantiate(prefabToBeSpawn, nozzlePoint.position, nozzlePoint.rotation);
                _reloadTimer = shootBullet.GetBulletReloadTime();
                shootBullet.WeaponActionToPoint(nozzlePoint, _rotationToMousePointByRay.GetMouseLookPosition());
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