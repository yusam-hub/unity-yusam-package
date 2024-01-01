using UnityEngine;

namespace YusamPackage
{
    public class MainMenuState : GameManagerState
    {
        [SerializeField] private GameMenu gameMenu;
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
            gameMenu.gameObject.SetActive(true);
        }
        
        public override void Exit()
        {
            gameMenuUI.SetActive(false);
            gameMenu.gameObject.SetActive(false);
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
            switch (menuKey)
            {
                case "start":
                    if (gameManager.currentManagerStateEnum != GameManager.GameManagerStateEnum.LoadingScene)
                    {
                        gameManager.currentManagerStateEnum = GameManager.GameManagerStateEnum.LoadingScene;
                    }
                    break;
                case "exit":
                case "quit":
                    Application.Quit();
                    break;
            }
        }
    }
}