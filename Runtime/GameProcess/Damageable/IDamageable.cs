using UnityEngine;

namespace YusamPackage
{
    public interface IDamageable
    {
        public void TakeDamage(float volume);
        public void TakeDamageWithPause(float volume);
    }
}