using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    public class Kosa : MonoBehaviour
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
            Collider[] collider = Physics.OverlapSphere(transform.position, 3f, 0);
            
            if (_debugProperties.enabled)
            {
                Debug.DrawLine(startPoint.position, endPoint.position, _debugProperties.debugDefaultColor, _debugProperties.debugDefaultDuration);
            }
        }
    }
}