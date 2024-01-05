using UnityEngine;

namespace YusamPackage
{
    public abstract class BaseWeaponSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("BASE WEAPON",3, "#FFFFFF", 16)]
#endif        
        [Space(10)]

        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Эффект при старте", 1)]
#endif        
        [Space(10)]
        public GameObject startEffectPrefab;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Время жизни эффекта при старте", 2)]
#endif        
        [Space(10)]
        public float startEffectDestroyTime = 1f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Какие слои проверяются на столкновения")]
#endif        
        [Space(10)]
        public LayerMask hitLayerMask;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Эффект при попадании", 1)]
#endif        
        [Space(10)]
        public GameObject hitEffectPrefab;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Время жизни эффекта при попадании", 2)]
#endif        
        [Space(10)]
        public float hitEffectDestroyTime = 1f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Объем повреждения здоровья от столкновения", 2)]
#endif        
        [Space(10)]
        public float hitDamageVolume = 10f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Сила повреждения от столкновения", 2)]
#endif        
        [Space(10)]
        public float hitDamageForce = 1f;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Время жизни скрипта")]
#endif        
        [Space(10)]
        public float scriptLifeTime = 100f;
    }
}