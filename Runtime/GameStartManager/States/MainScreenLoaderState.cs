using UnityEngine;

namespace YusamPackage
{
    public class MainScreenLoaderState : GameStartManagerState
    {
        [SerializeField] private GameObject mainScreenLoadeUi;
        [SerializeField] private float timerMax = 4f;

        private float _timer;

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
            if (isFinished) return;
            
            _timer += Time.deltaTime;
            if (_timer >= timerMax)
            {
                isFinished = true;
            }
        }

        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.MainScreenLoader;
        }

    }
}