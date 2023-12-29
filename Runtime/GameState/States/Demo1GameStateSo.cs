using UnityEngine;

namespace YusamPackage.GameState.Level.States
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game State/Demo/Demo 1")]
    public class Demo1GameStateSo : GameStateSo
    {
        [SerializeField] private float maxTimer = 1f;

        private float _currentTimer = 0;

        public override void Update()
        {
            if (IsFinished)
            {
                return;
            }

            DoSomething();
        }

        private void DoSomething()
        {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= maxTimer)
            {
                IsFinished = true;
            }
        }
    }
}
