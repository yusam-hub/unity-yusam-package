using UnityEngine;

namespace YusamPackage
{
    public static class DebugHelper
    {
        public static void DrawCircleXZ(Vector3 position, float radius, int segments, Color color, float duration)
        {
            if (radius <= 0.0f || segments <= 0)
            {
                return;
            }

            float angleStep = (360.0f / segments);
 
            angleStep *= Mathf.Deg2Rad;
 
            Vector3 lineStart = Vector3.zero;
            Vector3 lineEnd = Vector3.zero;
 
            for (int i = 0; i < segments; i++)
            {
                lineStart.x = Mathf.Cos(angleStep * i) ;
                lineStart.z = Mathf.Sin(angleStep * i);
                    
                lineEnd.x = Mathf.Cos(angleStep * (i + 1));
                lineEnd.z = Mathf.Sin(angleStep * (i + 1));

                lineStart *= radius;
                lineEnd *= radius;
 
                lineStart += position;
                lineEnd += position;
 
                Debug.DrawLine(lineStart, lineEnd, color, duration);
            }
        }        
    }
}