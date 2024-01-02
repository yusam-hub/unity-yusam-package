using UnityEngine;

namespace YusamPackage
{
    public interface IHealth
    {
        public float GetHealth();
        public void PlusHealth(float volume);
        public void MinusHealth(float volume);
    }
}