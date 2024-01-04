using UnityEngine;

namespace YusamPackage
{
    public interface IWeaponActionToPoint
    {
        public void WeaponActionToPoint(Transform sourceTransform, Vector3 destinationPoint);
    }
}