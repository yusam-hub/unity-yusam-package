using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    public class Sword : MonoBehaviour
    {
        [SerializeField] private SwordSo swordSo;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

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