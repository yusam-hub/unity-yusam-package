using UnityEngine;

namespace YusamPackage
{
    public class MainMenuState : GameManagerState
    {
        [SerializeField] private GameObject gameMenuUI;
        [SerializeField] private GameManager gameManager;

        private bool _isFinished = false;
        
        public override GameManager.GameManagerStateEnum GetGameManagerStateEnum()
        {
            return GameManager.GameManagerStateEnum.MainMenu;
        }
        
        public override void Enter()
        {
            gameMenuUI.SetActive(true);
        }
        
        public override void Exit()
        {
            gameMenuUI.SetActive(false);
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }

        public override void Update()
        {
            
        }
        
        public void OnGameMenuKeyEvent(string menuKey)
        {
            //бывает два раза прилетает событие одно и тоже
            GameDebug.Log(this.name + " : " + menuKey);
            switch (menuKey)
            {
                case "start":
                    gameManager.currentManagerStateEnum = GameManager.GameManagerStateEnum.LoadingScene;
                    break;
                case "exit":
                case "quit":
                    Application.Quit();
                    break;
            }
        }
    }
}