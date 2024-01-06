using UnityEngine;

namespace YusamPackage
{
    public interface IDamage
    {
        public void DoDamage(Collider hitCollider, float volume, float force);
        public void SelfDestroy(Collider hitCollider, float force);
    }
}