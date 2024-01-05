using UnityEngine;

namespace YusamPackage
{
    public static class TransformHelper
    {
        /*
         * Определяем куда смотрит вектор, направление вектора
         */
        public static Vector3 LookAt(Vector3 fromPosition, Vector3 toPosition)
        {
            return fromPosition + (toPosition - fromPosition);
        }

        /*
         * Создаем новую позицию векта endPosition от вектора startPosition на указанное расстояние от вектора endPosition
         */
        public static Vector3 NewEndPositionCalculateFromEndPosition(Vector3 startPosition, Vector3 endPosition, float distance)
        {
            return endPosition + (endPosition - startPosition).normalized * distance;
        }
        
        /*
         * Создаем новую позицию векта endPosition от вектора startPosition на указанное расстояние от веектора startPosition
         */
        public static Vector3 NewEndPositionCalculateFromStartPosition(Vector3 startPosition, Vector3 endPosition, float distance)
        {
            return startPosition + (endPosition - startPosition).normalized * distance;
        }
    }
}