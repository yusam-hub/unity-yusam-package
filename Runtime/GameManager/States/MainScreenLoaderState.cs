using UnityEngine;

namespace YusamPackage
{
    public class MainScreenLoaderState : GameManagerState
    {
        [SerializeField] private GameObject mainScreenLoadeUi;
        [SerializeField] private float timerMax = 4f;

        private float _timer;
        private bool _isFinished = false;

        public override void Enter()
        {
            _timer = 0;
            mainScreenLoadeUi.SetActive(true);
        }
        
        public override void Exit()
        {
            mainScreenLoadeUi.SetActive(false);
        }

        public override void Update()
        {
            if (_isFinished) return;
            
            _timer += Time.deltaTime;
            if (_timer >= timerMax)
            {
                _isFinished = true;
            }
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }
        
        public override GameManager.GameManagerStateEnum GetGameManagerStateEnum()
        {
            return GameManager.GameManagerStateEnum.MainScreenLoader;
        }

    }
}