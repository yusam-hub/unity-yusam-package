using UnityEngine;

namespace YusamPackage
{
    public abstract class GameInputPosition : MonoBehaviour, IGameInputPosition
    {
        public abstract Vector2 GetInputPosition();
    }
}