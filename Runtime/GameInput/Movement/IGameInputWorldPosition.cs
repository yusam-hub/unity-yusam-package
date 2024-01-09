using UnityEngine;

namespace YusamPackage
{
    public interface IGameInputWorldPosition
    {
        public Vector3 GetInputWorldPosition(Vector3 lastInputWorldPosition);
    }
}