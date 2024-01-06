using UnityEngine;

namespace YusamPackage
{
    public interface IDamage
    {
        public void DoDamage(Collider hitCollider, float volume, float force);
    }
}