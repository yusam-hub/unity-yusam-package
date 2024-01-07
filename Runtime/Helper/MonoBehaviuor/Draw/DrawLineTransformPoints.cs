using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    public class DrawLineTransformPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] pointArray;
        [SerializeField] private bool loopPoints;
        [SerializeField] private bool tubeEffectEnabled;
        [SerializeField] private int tubeCircleCount = 1;
        
        
        private DebugProperties _debugProperties;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }

        private void Update()
        {
            if (!_debugProperties.debugEnabled) return;
            if (pointArray.Length < 2) return;

            for(var i = 0; i < pointArray.Length; i++)
            {
                var startT = pointArray[i]; 
                if (i + 1 < pointArray.Length)
                {
                    var endT = pointArray[i + 1];
                    Debug.DrawLine(startT.position, endT.position, _debugProperties.debugLineColor, _debugProperties.debugDuration);
                    if (tubeEffectEnabled)
                    {
                        DebugHelper.DrawCircleTubeAlongToPoint(startT, endT, _debugProperties.debugLineColor,
                            _debugProperties.debugDuration, tubeCircleCount);
                    }
                }
                else if (loopPoints)
                {
                    var endT = pointArray[0];
                    Debug.DrawLine(startT.position, endT.position, _debugProperties.debugLineColor, _debugProperties.debugDuration);
                    
                    if (tubeEffectEnabled)
                    {
                        DebugHelper.DrawCircleTubeAlongToPoint(startT, endT, _debugProperties.debugLineColor,
                            _debugProperties.debugDuration, tubeCircleCount);
                    }
                }
            }
        }

    }
}