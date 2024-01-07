using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameInputController gameInputController;
        [SerializeField] private LookAtTargetPosition lookAtTargetPosition;
        [SerializeField] private Transform nozzlePoint;
        [SerializeField] private ShootBullet prefabToBeSpawn;
        [SerializeField] private GameInputPerformedEnum[] inputs;

        private float _reloadTimer;
        private bool _isReloading;
        
        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(lookAtTargetPosition == null,typeof(Transform) + " : Look At Target", this);
            LogErrorHelper.NotFoundWhatInIf(nozzlePoint == null,typeof(Transform) + " : Nozzle Point", this);
            LogErrorHelper.NotFoundWhatInIf(prefabToBeSpawn == null,typeof(ShootBullet) + " : Prefab To Be Spawn", this);

            if (gameInputController)
            {
                foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
                {
                    gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnInputAction;
                }
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
            if (!gameInputController.IsLayerAccessible()) return;
            Shoot();
        }

        public void Shoot()
        {
            if (!_isReloading)
            {
                _isReloading = true;
                ShootBullet shootBullet = Instantiate(prefabToBeSpawn, nozzlePoint.position, nozzlePoint.rotation);
                _reloadTimer = shootBullet.GetBulletReloadTime();
                shootBullet.WeaponActionToPoint(nozzlePoint, lookAtTargetPosition.GetLookAtTargetPosition());
            }
        }

        private void OnDestroy()
        {
            if (gameInputController)
            {
                foreach (GameInputPerformedEnum gameInputPerformedEnum in inputs)
                {
                    gameInputController.gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnInputAction;
                }
            }
        }
    }
}