using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Rotatable : MonoBehaviour, IRotatable
    {
        [SerializeField] private bool canRotate = true;
        
        public bool CanRotate()
        {
            return canRotate;
        }

        public void SetCanRotate(bool value)
        {
            canRotate = value;
        }

        public void SetTransformRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public Quaternion GetTransformRotation()
        {
            return transform.rotation;
        }
    }
}