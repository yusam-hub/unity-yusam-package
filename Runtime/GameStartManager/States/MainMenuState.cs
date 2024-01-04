using UnityEngine;

namespace YusamPackage
{
    public class MainMenuState : GameStartManagerState
    {
        [SerializeField] private GameMenu gameMenu;
        [SerializeField] private GameObject gameMenuUI;

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

        public void OnGameMenuKeyEvent(string menuKey)
        {
            switch (menuKey)
            {
                case "start":
                    isFinished = true;
                    //GetGameStartManager().currentManagerStateEnum = GameStartManager.GameStartManagerStateEnum.LoadingScene;
                    //Exit();
                    break;
                case "exit":
                case "quit":
                    Application.Quit();
                    break;
            }
        }
        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.MainMenu;
        }
    }
}