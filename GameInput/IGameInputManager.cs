using UnityEngine;

namespace YusamPackage.GameInput
{
    public interface IGameInputManager
    {
        public Vector2 GetLeftStickVector2Normalized();
        public Vector2 GetRightStickVector2Normalized();

    }
}