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
                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
