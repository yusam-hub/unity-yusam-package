using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Damageable")]
    public class DamageableSo : ScriptableObject
    {
        public string layerName = "Default";
        public bool replaceLayer = true;
        public float pauseTakeDamage = 0.3f;
    }
}