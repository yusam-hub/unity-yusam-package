using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health Zone")]
    public class HealthZoneSo : ScriptableObject
    {
        public float healthVolume = 1f;
        public float healthReUseInterval = 0.5f;
    }
}