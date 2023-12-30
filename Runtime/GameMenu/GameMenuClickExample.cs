using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YusamPackage.GameInput;

namespace YusamPackage.GameMenu
{
    public class GameMenuClickExample : MonoBehaviour
    {
        public void OnGameMenuUiKeyEvent(string menuKey)
        {
            Debug.Log(this.name + " : " + menuKey);
            switch (menuKey)
            {
                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
