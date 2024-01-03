using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Sword")]
    public class SwordSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Объем повреждения здоровья от удара", 2)]
        [Space(10)]
        public float hitDamageVolume = 10f;
        
        [Space(10)]
        [YusamHelpBox("Сила повреждения от удара", 2)]
        [Space(10)]
        public float hitDamageForce = 1f;
        
        [Space(10)]
        [YusamHelpBox("Длительность удара/анимации", 2)]
        [Space(10)]
        public float hitDamageDuration = 0.2f;
    }
}