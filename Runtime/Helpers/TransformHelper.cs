using UnityEngine;

namespace YusamPackage
{
    public static class TransformHelper
    {
        public static Vector3 LookAt(Vector3 fromPosition, Vector3 toPosition)
        {
            return fromPosition + (toPosition - fromPosition);
        }    
    }
}