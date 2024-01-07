using System;
using UnityEngine;

namespace YusamPackage.Raycast
{
    [RequireComponent(typeof(DebugProperties))]
    public class RaycastLine : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool drawRay = true;
        [SerializeField] private Color rayColor = Color.black;
        [SerializeField] private float hitDrawRadius = 0.2f;
        
        private DebugProperties _debugProperties;
        private Ray _ray;
        private void Awake()
        {
            _debugProperties = GetComponent<DebugProperties>();
        }

        private void Update()
        {
            if (!_debugProperties.debugEnabled) return;
            


            _ray = new Ray();
            _ray.origin = transform.position;
            _ray.direction = (target.position - _ray.origin).normalized;
            
            transform.rotation = Quaternion.LookRotation(_ray.direction);
            
            string[] dStrings = new[]
            {
                "",
                "",
                "",
                "",
                "",
                ""
            };

            if (drawRay)
            {
                Debug.DrawRay(_ray.origin, _ray.direction * maxDistance, rayColor,
                    _debugProperties.debugDuration);
            }

            if (Physics.Raycast(_ray, out var hit, maxDistance, layerMask))
            {
                int di = 0;
                dStrings[di++] = $"Hit Point: {hit.point}";
                dStrings[di++] = $"Hit Distance: {hit.distance}";
                dStrings[di++] = $"Hit Normal: {hit.normal}";
                dStrings[di++] = $"Collider Bounds: {hit.collider.bounds}";
                dStrings[di++] = $"Collider Position: {hit.collider.transform.position}";
                dStrings[di++] = $"Collider Rotation: {hit.collider.transform.rotation}";
                DebugHelper.DrawCrossNormal(hit.point, hit.normal, hitDrawRadius, _debugProperties.debugLineColor, _debugProperties.debugDuration);
            }

            for (var i = 0; i < dStrings.Length; i++)
            {
                DebugDisplay.Instance?.DisplayIndex(i, dStrings[i]);         
            }
        }

    }
}