using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    public class DrawCircleAxis : MonoBehaviour
    {
        private DebugProperties _debugProperties;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }

        private void Update()
        {
            if (!_debugProperties.debugEnabled) return;

            DrawTest();
        }
        
        
        private void DrawTest(float length = 1, float radius = 0.5f, int circleSegments = 8)
        {
            var startPos = transform.position;
            var endPos = startPos + Vector3.forward * 1;

            Vector3 normal = (startPos - endPos).normalized;
            Vector3 localNormal = transform.InverseTransformDirection(normal);

            Debug.DrawLine(startPos, startPos + localNormal * 1);
            Quaternion newRot = Quaternion.Euler(localNormal);

            DebugHelper.DrawCircle(startPos, newRot, radius, circleSegments, _debugProperties.debugLineColor, _debugProperties.debugDuration);
        }
    }
}