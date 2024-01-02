using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health")]
    public class HealthSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Максимальное значение здоровья",3)]
        [Space(10)]
        public float maxHealth = 100f;
    }
}