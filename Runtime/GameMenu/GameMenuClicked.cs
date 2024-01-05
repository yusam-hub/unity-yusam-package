using UnityEngine;

namespace YusamPackage
{
    public class GameMenuClicked : MonoBehaviour
    {
        public void OnDebugClicked(GameObject sender, string menuKey)
        {
            Debug.Log($"{GetType()} - {menuKey}");
        }
     
        public void OnIfResumeDoDisable(GameObject sender, string menuKey)
        {
            if (menuKey == "resume")
            {
                sender.SetActive(false);
            }
        }
        
        public void OnIfExitDoExitApplication(GameObject sender, string menuKey)
        {
            switch (menuKey)
            {
                case "exit":                
                case "quit":
                    ApplicationHelper.QuitBuildAndEditor();
                    break;
            }
        }
    }
}
