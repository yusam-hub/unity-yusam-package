using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Damage")]
    public class DamageSo : ScriptableObject
    {
        public GameObject hitEffectPrefab;
        public float hitEffectDestroyTime = 1f;
    }
}