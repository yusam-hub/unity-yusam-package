using UnityEngine;

namespace YusamPackage
{
    public static class DebugHelper
    {
        public static void DrawNormal(Vector3 center, Vector3 normal, float len, Color color, float duration)
        {
            Debug.DrawLine(center, center + normal * len, color, duration);
        }
        
        public static void DrawCross(Vector3 position, Quaternion rotation, float radius, Color color, float duration)
        {
            DrawCircle(position, rotation, radius, 8, color, duration, true);
        }
        
        public static void DrawCrossNormal(Vector3 position, Vector3 normal, float radius, Color color, float duration)
        {
            Quaternion rotation = Quaternion.LookRotation(normal);
            DrawCross(position, rotation, radius, color, duration);
            DrawNormal(position, normal, radius/2, color, duration);
        }
        
        public static void DrawCircle(Vector3 position, Quaternion rotation, float radius, int segments, Color color, float duration, bool withStart = false)
        {
            // If either radius or number of segments are less or equal to 0, skip drawing
            if (radius <= 0.0f || segments <= 0)
            {
                return;
            }
 
            // Single segment of the circle covers (360 / number of segments) degrees
            float angleStep = (360.0f / segments);
 
            // Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
            // which are required by Unity's Mathf class trigonometry methods
 
            angleStep *= Mathf.Deg2Rad;
 
            // lineStart and lineEnd variables are declared outside of the following for loop
            Vector3 lineStart = Vector3.zero;
            Vector3 lineEnd = Vector3.zero;
 
            for (int i = 0; i < segments; i++)
            {
                // Line start is defined as starting angle of the current segment (i)
                lineStart.x = Mathf.Cos(angleStep * i);
                lineStart.y = Mathf.Sin(angleStep * i);
                lineStart.z = 0.0f;
 
                // Line end is defined by the angle of the next segment (i+1)
                lineEnd.x = Mathf.Cos(angleStep * (i + 1));
                lineEnd.y = Mathf.Sin(angleStep * (i + 1));
                lineEnd.z = 0.0f;
 
                // Results are multiplied so they match the desired radius
                lineStart *= radius;
                lineEnd *= radius;
 
                // Results are multiplied by the rotation quaternion to rotate them 
                // since this operation is not commutative, result needs to be
                // reassigned, instead of using multiplication assignment operator (*=)
                lineStart = rotation * lineStart;
                lineEnd = rotation * lineEnd;
 
                // Results are offset by the desired position/origin 
                lineStart += position;
                lineEnd += position;
 
                // Points are connected using DrawLine method and using the passed color
                Debug.DrawLine(lineStart, lineEnd, color, duration);
                if (withStart)
                {
                    Debug.DrawLine(position, lineEnd, color, duration); 
                }
            }
        }
        
        public static void DrawCircleTubeAlongToPoint(Transform startT, Transform endT, Color color, float duration, int circleCount = 1, float radius = 0.5f, int circleSegments = 8)
        {
            var lookDirection = endT.position - startT.position;
            var distance = lookDirection.magnitude;
            var normal = lookDirection.normalized;
            var segmentDistance = distance / (circleCount + 1);
            Quaternion newRot = Quaternion.LookRotation(normal);

            Vector3 tmpPoint = startT.position;
            for (var i = 0; i < circleCount; i++)
            {
                tmpPoint += normal * segmentDistance;
                DrawCircle(tmpPoint, newRot, radius, circleSegments, color, duration);
            }
        }
        
        public static void DrawSphere(Vector3 position, Quaternion orientation, float radius, Color color, float duration, int segments = 4)
        {
            if(segments < 2)
            {
                segments = 2;
            }
 
            int doubleSegments = segments * 2;
            float meridianStep = 180.0f / segments;
 
            for(int i = 0; i < segments; i++)
            {
                DrawCircle(position, Quaternion.Euler(0, meridianStep * i, 0), radius, doubleSegments, color, duration);
            }
        }
        
        public static void DrawQuad(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD, Color color, float duration = 0)
        {
            // Draw lines between the points
            Debug.DrawLine(pointA, pointB, color, duration);
            Debug.DrawLine(pointB, pointC, color, duration);
            Debug.DrawLine(pointC, pointD, color, duration);
            Debug.DrawLine(pointD, pointA, color, duration);
        }
        
        public static void DrawRectangle(Vector3 position, Quaternion orientation, Vector2 extent, Color color, float duration = 0)
        {
            Vector3 rightOffset = Vector3.right * extent.x * 0.5f;
            Vector3 upOffset = Vector3.up * extent.y * 0.5f;
 
            Vector3 offsetA = orientation * (rightOffset + upOffset);
            Vector3 offsetB = orientation * (-rightOffset + upOffset);
            Vector3 offsetC = orientation * (-rightOffset - upOffset);
            Vector3 offsetD = orientation * (rightOffset - upOffset);
 
            DrawQuad(position + offsetA,
                position + offsetB,
                position + offsetC,
                position + offsetD, 
                color, duration);
        }
        
        public static void DrawRectangle(Vector2 point1, Vector2 point2, Vector3 origin, Quaternion orientation, Color color)
        {
            // Calculate extent as a distance between point1 and point2
            float extentX = Mathf.Abs(point1.x - point2.x);
            float extentY = Mathf.Abs(point1.y - point2.y);
 
            // Calculate rotated axes
            Vector3 rotatedRight = orientation * Vector3.right;
            Vector3 rotatedUp = orientation * Vector3.up;
         
            // Calculate each rectangle point
            Vector3 pointA = origin + rotatedRight * point1.x + rotatedUp * point1.y;
            Vector3 pointB = pointA + rotatedRight * extentX;
            Vector3 pointC = pointB + rotatedUp * extentY;
            Vector3 pointD = pointA + rotatedUp * extentY;
 
            DrawQuad(pointA, pointB, pointC, pointD, color);
        }
        
        public static void DrawBox(Vector3 position, Quaternion orientation, Vector3 size, Color color)
        {
            Vector3 offsetX = orientation * Vector3.right * size.x * 0.5f;
            Vector3 offsetY = orientation * Vector3.up * size.y * 0.5f;
            Vector3 offsetZ = orientation * Vector3.forward * size.z * 0.5f;
 
            Vector3 pointA = -offsetX + offsetY;
            Vector3 pointB = offsetX + offsetY;
            Vector3 pointC = offsetX - offsetY;
            Vector3 pointD = -offsetX - offsetY;
 
            DrawRectangle(position - offsetZ, orientation, new Vector2(size.x, size.y), color);
            DrawRectangle(position + offsetZ, orientation, new Vector2(size.x, size.y), color);
 
            Debug.DrawLine(pointA - offsetZ, pointA + offsetZ, color);
            Debug.DrawLine(pointB - offsetZ, pointB + offsetZ, color);
            Debug.DrawLine(pointC - offsetZ, pointC + offsetZ, color);
            Debug.DrawLine(pointD - offsetZ, pointD + offsetZ, color);
        }
        
        public static void DrawCube(Vector3 position, float size)
        {
            Vector3 offsetX = Vector3.right * size * 0.5f;
            Vector3 offsetY = Vector3.up * size * 0.5f;
            Vector3 offsetZ = Vector3.forward * size * 0.5f;
 
            Vector3 pointA = -offsetX + offsetY;
            Vector3 pointB = offsetX + offsetY;
            Vector3 pointC = offsetX - offsetY;
            Vector3 pointD = -offsetX - offsetY;
 
            DrawRectangle(position - offsetZ, Quaternion.identity, Vector2.one * size, Color.red);
            DrawRectangle(position + offsetZ, Quaternion.identity, Vector2.one * size, Color.blue);
 
            Debug.DrawLine(pointA - offsetZ, pointA + offsetZ, Color.green);
            Debug.DrawLine(pointB - offsetZ, pointB + offsetZ, Color.green);
            Debug.DrawLine(pointC - offsetZ, pointC + offsetZ, Color.green);
            Debug.DrawLine(pointD - offsetZ, pointD + offsetZ, Color.green);
        }
        
        public static void DrawTriangle(Vector3 pointA, Vector3 pointB, Vector3 pointC, Color color)
        {
            // Connect pointA and pointB
            Debug.DrawLine(pointA, pointB, color);
 
            // Connect pointB and pointC
            Debug.DrawLine(pointB, pointC, color);
 
            // Connect pointC and pointA
            Debug.DrawLine(pointC, pointA, color);
        }
    }
}