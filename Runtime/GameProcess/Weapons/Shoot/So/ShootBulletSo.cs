using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shoot Bullet")]
    public class ShootBulletSo : BaseWeaponSo
    {
        public enum ShootBulletTrajectory
        {
            LinerTrajectory, //от точки а до б
            ParabolaTrajectory, //от точки а до б по параболе
            ParallelTrajectory //от точки а и по направлению Б параллельно
        }
        public ShootBulletTrajectory trajectory = ShootBulletTrajectory.LinerTrajectory;
        public bool rotateToTrajectory = true;
        
        public float parallelMaxDistance = 20f;
        public float parallelMinDistance = 2f;
        public float parabolaHeight = 2;
        
        public float bulletSpeed = 20;
        public float bulletHitRadius = .05f;
        public float bulletReloadTime = .5f;
        
        //public float scriptLifeTime;
        
        public GameObject startEffectPrefab;
        public float startEffectDestroyTime = 1f;
        
        public GameObject hitEffectPrefab;
        public float hitEffectDestroyTime = 1f;
        
        public LayerMask hitDamageLayerMask;
        public float hitDamageVolume = 10f;
        public float hitDamageForce = 1f;
    }
}