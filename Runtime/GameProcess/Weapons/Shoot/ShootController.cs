using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    [DisallowMultipleComponent]
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameInputWorldPosition gameInputWorldPosition;
        [SerializeField] private Transform nozzlePoint;
        [SerializeField] private ShootBullet prefabToBeSpawn;
        [SerializeField] private ShootBulletSo shootBulletSo;
        
        private float _reloadTimer;
        private bool _isReloading;
        
        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(nozzlePoint == null,typeof(Transform) + " : Nozzle Point", this);
            LogErrorHelper.NotFoundWhatInIf(prefabToBeSpawn == null,typeof(ShootBullet) + " : Prefab To Be Spawn", this);
        }
        
        public void SetShootBulletSo(ShootBulletSo newShootBulletSo)
        {
            shootBulletSo = newShootBulletSo;
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

        public void Shoot()
        {
            if (!_isReloading)
            {
                _isReloading = true;
                ShootBullet shootBullet = Instantiate(prefabToBeSpawn, nozzlePoint.position, nozzlePoint.rotation);
                shootBullet.SetShootBulletSo(shootBulletSo);
                
                _reloadTimer = shootBullet.GetBulletReloadTime();
                shootBullet.WeaponActionToPoint(nozzlePoint, gameInputWorldPosition.GetInputWorldPosition());
            }
        }
    }
}