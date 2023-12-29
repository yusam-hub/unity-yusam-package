using UnityEngine;

namespace YusamPackage.GameState.So
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game State/Demo/Demo 1")]
    public class Demo1GameStateSo : GameStateSo
    {
        [SerializeField] private float maxTimer = 1f;
        [SerializeField] private Demo2GameStateSo demo2GameStateSo;
        
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
                gameStateUpdateMachineSo.SetGameStateSo(demo2GameStateSo);
            }
        }
    }
}
