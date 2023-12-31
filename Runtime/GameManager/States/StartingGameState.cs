using UnityEngine;

namespace YusamPackage
{
    public class StartingGameState : GameManagerState
    {
        [SerializeField] private GameObject startingGameUi;
        [SerializeField] private float timerMax = 2f;

        private float _timer;
        private bool _isFinished = false;

        public override void Enter()
        {
            _timer = 0;
            startingGameUi.SetActive(true);
        }
        
        public override void Exit()
        {
            startingGameUi.SetActive(false);
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
            return GameManager.GameManagerStateEnum.StartingGame;
        }
    }
}