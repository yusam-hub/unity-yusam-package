using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    public class DrawLineAxis : MonoBehaviour
    {
        [SerializeField] private float lineLength = 2;

        private DebugProperties _debugProperties;

        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }

        private void Update()
        {
            if (!_debugProperties.debugEnabled) return;

            var tmp = transform;
            var startPoint = tmp.position;
            Debug.DrawRay(startPoint, tmp.forward * lineLength, Color.blue, _debugProperties.debugDuration);
            Debug.DrawRay(startPoint, tmp.up * lineLength, Color.green, _debugProperties.debugDuration);
            Debug.DrawRay(startPoint, tmp.right * lineLength, Color.red, _debugProperties.debugDuration);
        }
    }
}