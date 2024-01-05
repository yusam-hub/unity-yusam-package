using UnityEngine;

namespace YusamPackage
{
    public class MainMenuState : GameStartManagerState
    {
        [SerializeField] private GameObject gameMenuUI;

        public override void Enter()
        {
            gameMenuUI.SetActive(true);
        }
        
        public override void Exit()
        {
            gameMenuUI.SetActive(false);
        }
                
        public void OnIfStart(GameObject sender, string menuKey)
        {
            switch (menuKey)
            {
                case "start":
                    isFinished = true;
                    break;                
            }
        }
        
        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.MainMenu;
        }
    }
}