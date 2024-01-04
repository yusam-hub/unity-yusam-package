using UnityEngine;

namespace YusamPackage
{
    public class GameMenuClickExample : MonoBehaviour
    {
        public void OnGameMenuUiKeyEvent(string menuKey)
        {
            Debug.Log($"{GetType()} - OnGameMenuUiKeyEvent {menuKey}");
            switch (menuKey)
            {
                case "resume":
                    Time.timeScale = 1;
                    break;
                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
