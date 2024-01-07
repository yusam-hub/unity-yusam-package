using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shield")]
    public class ShieldSo : BaseWeaponSo
    {
        public GameObject prefabOnActivateShield;
        public float shieldActiveLifeTime = 15f;
        public float shieldReloadLifeTime = 10f;

        public GameObject prefabOnDestroyShield;
        public float lifeTimeOnDestroyShield = 5f;

        public LayerMask layerMaskOnDestroyShield;
        public float radiusOnDestroyShield = 5f;
        public float damageVolumeOnDestroyShield = 10f;
    }
}