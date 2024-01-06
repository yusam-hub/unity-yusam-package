using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(DebugProperties))]
    public class DrawLineForward : MonoBehaviour
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

            Debug.DrawRay(transform.position, transform.forward * lineLength, _debugProperties.debugLineColor, _debugProperties.debugDuration);
        }
    }
}