using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Death Zone")]
    public class DeathZoneSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Повреждение", 2)]
        [Space(10)]
        public float damageVolume = 1f;

        [Space(10)]
        [YusamHelpBox("Сила", 2)]
        [Space(10)]
        public float damageForce = 1f;
        
        [Space(10)]
        [YusamHelpBox("Интервал для нанесения следующего урона", 3)]
        [Space(10)]
        public float damageReUseInterval = 0.5f;
        
    }
}