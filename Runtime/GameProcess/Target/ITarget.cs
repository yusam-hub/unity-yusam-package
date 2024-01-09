using UnityEngine;

namespace YusamPackage
{
    public interface ITarget
    {
        public Transform GetTarget();
        public void SetTarget(Transform value);
    }
}