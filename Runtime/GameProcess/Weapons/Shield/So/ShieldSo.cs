using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shield")]
    public class ShieldSo : BaseWeaponSo
    {
        public float activeLifeTime = 5f;
        
        public GameObject endEffectPrefab;
        public float endEffectDestroyTime = 1f;
    }
}