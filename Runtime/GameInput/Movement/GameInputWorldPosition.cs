using UnityEngine;

namespace YusamPackage
{
    public abstract class GameInputWorldPosition : MonoBehaviour, IGameInputWorldPosition
    {
        public abstract Vector3 GetInputWorldPosition(Vector3 lastInputWorldPosition);
    }
}