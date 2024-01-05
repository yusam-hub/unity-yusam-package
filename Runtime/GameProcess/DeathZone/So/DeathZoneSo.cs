using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Death Zone")]
    public class DeathZoneSo : ScriptableObject
    {
        public float damageVolume = 1f;
        public float damageForce = 1f;
        public float damageReUseInterval = 0.5f;
    }
}