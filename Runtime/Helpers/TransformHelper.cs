using UnityEngine;

namespace YusamPackage
{
    public static class TransformHelper
    {
        public static Vector3 LookAt(Vector3 fromPosition, Vector3 toPosition)
        {
            return fromPosition + (toPosition - fromPosition);
        }

        public static Vector3 ChangeEndPosition(Vector3 startPosition, Vector3 endPosition, float distance)
        {
            return endPosition + (endPosition - startPosition) * distance;
        }
        
        public static Vector3 NewEndPosition(Vector3 startPosition, Vector3 endPosition, float distance)
        {
            return startPosition + (endPosition - startPosition).normalized * distance;
        }
    }
}