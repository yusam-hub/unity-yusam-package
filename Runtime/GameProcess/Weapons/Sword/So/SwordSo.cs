using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Sword")]
    public class SwordSo : BaseWeaponSo
    {
        public GameObject startEffectPrefab;
        public float startEffectDestroyTime = 1f;
        
        public GameObject hitEffectPrefab;
        public float hitEffectDestroyTime = 1f;
        
        public LayerMask hitDamageLayerMask;
        public float hitDamageVolume = 10f;
        public float hitDamageForce = 1f;
        
        public float hitDamageDuration = 0.2f;
        public float attackSpeed = 0.1f;
    }
}