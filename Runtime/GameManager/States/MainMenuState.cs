using UnityEngine;

namespace YusamPackage
{
    public class MainMenuState : GameManagerState
    {
        [SerializeField] private GameObject gameMenuUI;
        
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

        public override void Update()
        {
            
        }
        
        public void OnGameMenuKeyEvent(string menuKey)
        {
            //бывает два раза прилетает событие одно и тоже
            GameDebug.Log(this.name + " : " + menuKey);
            switch (menuKey)
            {
                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}