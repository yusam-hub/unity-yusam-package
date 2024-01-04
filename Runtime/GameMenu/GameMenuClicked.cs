using UnityEngine;

namespace YusamPackage
{
    public class GameMenuClicked : MonoBehaviour
    {
        public void OnGameMenuClicked(string menuKey)
        {
            Debug.Log($"{GetType()} - OnGameMenuClicked {menuKey}");
            switch (menuKey)
            {
                case "return":
                    Debug.Log($"Load main menu scene");
                    break;
                case "exit":
                    Application.Quit();
                    break;
            }
        }
        
    }
}
