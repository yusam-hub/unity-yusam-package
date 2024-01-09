using UnityEngine;

namespace YusamPackage
{
    public interface IRotatable
    {
        public bool CanRotate();

        public void SetCanRotate(bool value);

        public void SetTransformRotation(Quaternion rotation);

        public Quaternion GetTransformRotation();
    }
}