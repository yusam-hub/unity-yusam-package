using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(Target))]
    [DisallowMultipleComponent]
    public class TargetWorldPosition : GameInputWorldPosition
    {
        private Target _target;
        private void Awake()
        {
            _target = GetComponent<Target>();
        }
        
        public override Vector3 GetInputWorldPosition()
        {
            return _target.GetTarget().position;
        }
    }
}