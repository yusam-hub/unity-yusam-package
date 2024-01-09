using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Target : MonoBehaviour, ITarget
    {
        [SerializeField] private Transform target;
        
        public Transform GetTarget()
        {
            return target;
        }

        public void SetTarget(Transform value)
        {
            target = value;
        }
    }
}