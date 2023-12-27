using UnityEngine;

namespace YusamPackage.GameInput
{
    public interface IGameInputManager
    {
        public Vector2 GetMoveAsVector2Normalized();

        public Vector3 GetMoveAsHorizontalVector3Normalized();
    }
}