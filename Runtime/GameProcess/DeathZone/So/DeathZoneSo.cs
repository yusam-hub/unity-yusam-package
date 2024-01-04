using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Death Zone")]
    public class DeathZoneSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("DEATH ZONE",3, "#FFFFFF", 16)]
#endif        
        [Space(10)]
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Повреждение", 2)]
#endif        
        [Space(10)]
        public float damageVolume = 1f;

        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Сила", 2)]
#endif        
        [Space(10)]
        public float damageForce = 1f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Интервал для нанесения следующего урона", 3)]
#endif        
        [Space(10)]
        public float damageReUseInterval = 0.5f;
        
    }
}