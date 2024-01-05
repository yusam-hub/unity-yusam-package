using UnityEngine;

namespace YusamPackage
{
    public abstract class BaseWeaponSo : ScriptableObject
    {
        public GameObject startEffectPrefab;
        public float startEffectDestroyTime = 1f;
        
        public GameObject hitEffectPrefab;
        public float hitEffectDestroyTime = 1f;
        
        public LayerMask hitDamageLayerMask;
        public float hitDamageVolume = 10f;
        public float hitDamageForce = 1f;
        

    }
}