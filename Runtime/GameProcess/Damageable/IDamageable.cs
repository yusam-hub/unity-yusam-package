using UnityEngine;

namespace YusamPackage
{
    public interface IDamageable
    {
        public void TakeDamage(float volume);
        public void TakeDamage(float volume, Collider hitCollider);
        public void TakeDamage(float volume, Collider hitCollider, float force);
    }
}