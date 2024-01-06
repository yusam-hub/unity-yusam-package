using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Health))]
    public class Damage : MonoBehaviour, IDamage
    {
        [SerializeField] private DamageSo damageSo;

        private IHealth _health;
        private IDamage _parentDamage;
        private bool _selfDestroying;

        public void SetParentDamage(IDamage parentDamage)
        {
            _parentDamage = parentDamage;
        }
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        public void DoDamage(Collider hitCollider, float volume, float force)
        {
            _health.MinusHealth(volume);
        }
    }
}