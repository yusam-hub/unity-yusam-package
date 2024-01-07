using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YusamPackage
{
    public class IntervalEvent : MonoBehaviour
    {
        [SerializeField] private float intervalMax = 1f;
        [SerializeField] private float intervalMin = 1f;
        [SerializeField] private EmptyUnityEvent onIntervalEvent;

        private float _intervalTimer;
        private float _interval;

        private void Awake()
        {
            ChangeInterval();
        }

        private void ChangeInterval()
        {
            _interval = Random.Range(intervalMin, intervalMax);
        }

        private void Update()
        {
            _intervalTimer += Time.deltaTime;
            if (_intervalTimer >= _interval)
            {
                _intervalTimer = 0;
                onIntervalEvent?.Invoke(EventArgs.Empty);
                ChangeInterval();
            }
        }
    }
}