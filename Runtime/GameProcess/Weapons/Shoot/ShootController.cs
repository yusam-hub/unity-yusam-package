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
        private bool _isReloading;
        
        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(nozzlePoint == null,typeof(Transform) + " : Nozzle Point", this);
            LogErrorHelper.NotFoundWhatInIf(nozzlePoint == null,typeof(ShootBullet) + " : Prefab To Be Spawn", this);
            
            _gameInputController = GetComponent<GameInputController>();
            _rotationToMousePointByRay = GetComponent<RotationToMousePointByRay>();

            foreach(GameInputPerformedEnum gameInputPerformedEnum in inputs)
            {
                _gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
            }
        }

        private void Update()
        {
            if (_isReloading)
            {
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer < 0)
                {
                    _reloadTimer = 0;
                    _isReloading = false;
                }
            }
        }

        private void OnInputAction(InputAction.CallbackContext obj)
        {
            if (!_gameInputController.IsLayerAccessible()) return;
            
            if (!_isReloading)
            {
                _isReloading = true;
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