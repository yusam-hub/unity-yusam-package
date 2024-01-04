using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game State/Demo/Demo 1")]
    public class Demo1GameStateSo : GameStateSo
    {
        [SerializeField] private float maxTimer = 1f;
        [SerializeField] private Demo2GameStateSo demo2GameStateSo;
        
        private float _currentTimer;

        public override void Update()
        {
            DoSomething();
        }

        private void DoSomething()
        {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= maxTimer)
            {
                _currentTimer = 0;
                gameStateMachineSo.SetGameStateSo(demo2GameStateSo);
            }
        }
    }
}
