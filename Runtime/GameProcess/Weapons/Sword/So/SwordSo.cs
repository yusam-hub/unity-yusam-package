using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Sword")]
    public class SwordSo : BaseWeaponSo
    {
        [Space(10)]
        [Space(10)]
        [YusamHelpBox("SWORD",3, "#FFFFFF", 16)]
        [Space(10)]
        
        [Space(10)]
        [YusamHelpBox("Длительность удара/анимации", 2)]
        [Space(10)]
        public float hitDamageDuration = 0.2f;
    }
}