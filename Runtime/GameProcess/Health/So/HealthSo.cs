using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health")]
    public class HealthSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("HEALTH",3, "#FFFFFF", 16)]
        [Space(10)]
        
        [Space(10)]
        [YusamHelpBox("Максимальное значение здоровья",3)]
        [Space(10)]
        public float maxHealth = 100f;
        
        [Space(10)]
        [YusamHelpBox("Эффект смерти",3)]
        [Space(10)]
        public GameObject dieEffectPrefab;
        
        [Space(10)]
        [YusamHelpBox("Время жизни эффекта при смерти", 2)]
        [Space(10)]
        public float dieEffectDestroyTime = 10f;
        
        [Space(10)]
        [YusamHelpBox("Поворот объекта вокруг оси Y (0 - 360)", 2)]
        [Space(10)]
        public float dieEffectRotateAngleStartY = 0f;
        public float dieEffectRotateAngleEndY = 0f;
    }
}