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

            var angleStep = (360.0f / segments);
 
            angleStep *= Mathf.Deg2Rad;
 
            var lineStart = Vector3.zero;
            var lineEnd = Vector3.zero;
 
            for (var i = 0; i < segments; i++)
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