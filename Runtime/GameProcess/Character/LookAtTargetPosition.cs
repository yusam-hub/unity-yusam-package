using UnityEngine;

namespace YusamPackage
{
    public abstract class LookAtTargetPosition : MonoBehaviour, ILookAtTargetPosition
    {
        public abstract Vector3 GetMousePositionAsVector3();
    }
}