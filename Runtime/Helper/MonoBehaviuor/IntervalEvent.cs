using System;
using UnityEngine;

namespace YusamPackage
{
    public class IntervalEvent : MonoBehaviour
    {
        [SerializeField] private float interval = 2f;
        [SerializeField] private EmptyUnityEvent onIntervalEvent;

        private float _intervalTimer;
        
        private void Update()
        {
            _intervalTimer += Time.deltaTime;
            if (_intervalTimer >= interval)
            {
                _intervalTimer = 0;
                onIntervalEvent?.Invoke(EventArgs.Empty);
            }
        }
    }
}