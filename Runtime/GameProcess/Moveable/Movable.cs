using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Movable : MonoBehaviour, IMovable
    {
        [SerializeField] private bool canMoving = true;
        
        public bool CanMoving()
        {
            return canMoving;
        }

        public void SetCanMoving(bool value)
        {
            canMoving = value;
        }

        public void SetTransformPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetTransformPosition()
        {
            return transform.position;
        }
    }
}