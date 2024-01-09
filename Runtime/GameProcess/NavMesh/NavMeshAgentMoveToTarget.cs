using System;
using UnityEngine;
using UnityEngine.AI;

namespace YusamPackage
{
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Target))]
    [DisallowMultipleComponent]
    public class NavMeshAgentMoveToTarget : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Movable _movable;
        private Target _target;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = GetComponent<Target>();
            _movable = GetComponent<Movable>();
        }

        private void Update()
        {
            if (_target.GetTarget())
            {
                if (_movable.CanMoving())
                {
                    _agent.destination = _target.GetTarget().position;
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