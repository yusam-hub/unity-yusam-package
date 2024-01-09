using UnityEngine;

namespace YusamPackage
{
    public abstract class GameInputDirection : MonoBehaviour, IGameInputDirection
    {
        public abstract Vector2 GetInputDirection();
    }
}