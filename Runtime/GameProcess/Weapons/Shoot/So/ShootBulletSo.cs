using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shoot Bullet")]
    public class ShootBulletSo : ScriptableObject
    {
        public enum ShootBulletTrajectory
        {
            LinerTrajectory, //от точки а до б
            ParabolaTrajectory, //от точки а до б по параболе
            ParallelTrajectory, //от точки а и по направлению Б параллельно
        }

        [Space(10)]
        [YusamHelpBox("Какие слои проверяются на столкновения",3)]
        [Space(10)]
        public LayerMask bulletHitLayerMask;

        [Space(10)]
        [YusamHelpBox("Тип траектории",1)]
        [Space(10)]
        public ShootBulletTrajectory trajectory = ShootBulletTrajectory.LinerTrajectory;

        [Space(10)]
        [YusamHelpBox("Вращает объект для параболы",1)]
        [Space(10)]
        public bool rotateToTrajectory = true;

        [Space(10)]
        [YusamHelpBox("Максимальная дистанция при палаллельном типе траектории",3)]
        [Space(10)]
        public float parallelMaxDistance = 20f;

        [Space(10)]
        [YusamHelpBox("Если расстояние меньше чем тут указано, то включается параллельний тип траектории, 0 - отключает проверку",3)]
        [Space(10)]
        public float parallelMinDistance = 2f;
        
        [Space(10)]
        [YusamHelpBox("Высота параболической траектории",3)]
        [Space(10)]
        public float parabolaHeight = 2;
        
        [Space(10)]
        [YusamHelpBox("Скорость снаряда",3)]
        [Space(10)]
        public float bulletSpeed = 20;
       
        [Space(10)]
        [YusamHelpBox("Радиус проверки на столкновения",3)]
        [Space(10)]
        public float bulletHitRadius = .05f;
        
        [Space(10)]
        [YusamHelpBox("Время жизни снаряда",3)]
        [Space(10)]
        public float bulletLifeTime = 1f;


        [Space(10)]
        [YusamHelpBox("Эффект при старте", 1)]
        [Space(10)]
        public GameObject startEffectPrefab;
        
        [Space(10)]
        [YusamHelpBox("Время жизни эффекта при старте", 2)]
        [Space(10)]
        public float startEffectDestroyTime = 1f;
        
        [Space(10)]
        [YusamHelpBox("Эффект при попадании", 1)]
        [Space(10)]
        public GameObject hitEffectPrefab;
        
        [Space(10)]
        [YusamHelpBox("Время жизни эффекта при попадании", 2)]
        [Space(10)]
        public float hitEffectDestroyTime = 1f;
        
        [Space(10)]
        [YusamHelpBox("Объем повреждения здоровья от столкновения", 2)]
        [Space(10)]
        public float hitDamageVolume = 10f;
        
        [Space(10)]
        [YusamHelpBox("Сила повреждения от столкновения", 2)]
        [Space(10)]
        public float hitDamageForce = 1f;
    }
}