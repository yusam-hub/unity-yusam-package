using UnityEngine;

namespace YusamPackage
{
    public class GameMenuClickExample : MonoBehaviour
    {
        public void OnGameMenuUiKeyEvent(string menuKey)
        {
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
