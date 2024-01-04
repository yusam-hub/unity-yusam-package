using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Damage")]
    public class DamageSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("DAMAGE",3, "#FFFFFF", 16)]
#endif        
        [Space(10)]
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Эффект при попадании", 1)]
#endif        
        [Space(10)]
        public GameObject hitEffectPrefab;
        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Продолжительность жизни скрипта эффекта", 3)]
#endif        
        [Space(10)]
        public float hitEffectDestroyTime = 1f;
    }
}