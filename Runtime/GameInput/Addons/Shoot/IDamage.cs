using UnityEngine;

namespace YusamPackage
{
    public interface IDamage
    {
        public void DoDamage(Collider collider, float volume, float force);
    }
}