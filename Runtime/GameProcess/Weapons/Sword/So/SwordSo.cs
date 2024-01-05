using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Sword")]
    public class SwordSo : BaseWeaponSo
    {
        [Space(10)]
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("SWORD",3, "#FFFFFF", 16)]
#endif        
        [Space(10)]
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Длительность удара/анимации", 2)]
#endif        
        [Space(10)]
        public float hitDamageDuration = 0.2f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Скорость атаки")]
#endif        
        [Space(10)]
        public float attackSpeed = 0.1f;
    }
}