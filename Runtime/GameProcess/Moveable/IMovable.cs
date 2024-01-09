using UnityEngine;

namespace YusamPackage
{
    public interface IMovable
    {
        public bool CanMoving();
        public void SetCanMoving(bool value);
        public void SetTransformPosition(Vector3 position);
        public Vector3 GetTransformPosition();
    }
}