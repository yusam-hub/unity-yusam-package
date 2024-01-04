using UnityEngine;

namespace YusamPackage
{
    public class StartingGameState : GameStartManagerState
    {
        [SerializeField] private GameObject startingGameUi;
        [SerializeField] private float timerMax = 2f;

        private float _timer;
 
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
            if (isFinished) return;
            
            _timer += Time.deltaTime;
            if (_timer >= timerMax)
            {
                isFinished = true;
            }
        }

        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.StartingGame;
        }
    }
}