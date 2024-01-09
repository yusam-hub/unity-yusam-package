using System;
using UnityEngine;
using UnityEngine.AI;

namespace YusamPackage
{
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private NavMeshAgent _agent;
        private Movable _movable;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _movable = GetComponent<Movable>();
        }

        private void Update()
        {
            if (target)
            {
                if (_movable.CanMoving())
                {
                    _agent.destination = target.position;
                }
                else
                {
                    _agent.isStopped = true;        
                }
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }
}