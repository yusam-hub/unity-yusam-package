using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shield")]
    public class ShieldSo : BaseWeaponSo
    {
        public float shieldActiveLifeTime = 5f;
        public float shieldReloadLifeDeltaTime = .2f;
        
        public GameObject endEffectPrefab;
        public float endEffectDestroyTime = 1f;
    }
}