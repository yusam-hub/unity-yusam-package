using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    public class Sword : MonoBehaviour
    {
        [SerializeField] public Transform startPoint;
        [SerializeField] public Transform endPoint;

        private YusamDebugProperties _debugProperties;

        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
        }

        private void Update()
        {
            if (_debugProperties.enabled)
            {
                Debug.DrawLine(startPoint.position, endPoint.position, _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
            }
        }
    }
}