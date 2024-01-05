using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health")]
    public class HealthSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("HEALTH",3, "#FFFFFF", 16)]
#endif        
        [Space(10)]
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Максимальное значение здоровья")]
#endif        
        [Space(10)]
        public float maxHealth = 100f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Эффект смерти")]
#endif        
        [Space(10)]
        public GameObject dieEffectPrefab;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Время жизни эффекта при смерти", 2)]
#endif        
        [Space(10)]
        public float dieEffectDestroyTime = 10f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Поворот объекта вокруг оси Y (0 - 360)", 2)]
#endif        
        [Space(10)]
        public float dieEffectRotateAngleStartY;
        public float dieEffectRotateAngleEndY;
    }
}