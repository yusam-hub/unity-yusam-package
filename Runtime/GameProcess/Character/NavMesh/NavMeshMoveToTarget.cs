using System;
using UnityEngine;
using UnityEngine.AI;

namespace YusamPackage
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (target)
            {
                _agent.destination = target.position;
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }
}