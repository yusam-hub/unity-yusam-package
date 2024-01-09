using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public class Healthly : MonoBehaviour, IHealthly
    {
        private IHealth _health;
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        public void TakeHealth(float volume)
        {
            _health.PlusHealth(volume);
        }
    }
}