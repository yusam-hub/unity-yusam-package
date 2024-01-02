using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/ShootBullet")]
    public class ShootBulletSo : ScriptableObject
    {
        public enum ShootBulletTrajectory
        {
            LinerTrajectory, //от точки а до б
            ParabolaTrajectory, //от точки а до б по параболе
            ParallelTrajectory, //от точки а и по направлению Б параллельно
        }

        public ShootBulletTrajectory trajectory = ShootBulletTrajectory.LinerTrajectory;

        public bool turnToTrajectory = true;
      
        public float parallelMaxDistance = 20f;

        public float parabolaMinDistance = 2f;
        
        public float parabolaHeight = 2;
        
        public float bulletSpeed = 20;
        
        public float bulletHitRadius = .2f;

        public LayerMask bulletHitLayerMask;
    }
}