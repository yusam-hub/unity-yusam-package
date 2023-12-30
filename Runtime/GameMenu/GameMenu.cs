using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace YusamPackage.GameMenu
{
    public class GameMenu : MonoBehaviour
    {
        [Serializable]
        public struct GameMenuStruct
        {
            public string menuKey;
            public string menuText;
        }
        
        public event EventHandler<OnChangeGameMenuEventArgs> OnChangeGameMenu;
        public class OnChangeGameMenuEventArgs : EventArgs
        {
            public GameMenuStruct[] GameMenuStructArray;
        }
        
        public event EventHandler<OnSelectGameMenuEventArgs> OnSelectGameMenu;
        public class OnSelectGameMenuEventArgs : EventArgs
        {
            public int oldIndex;
            public int newIndex;
        }
        
        public event EventHandler<OnClickGameMenuEventArgs> OnClickGameMenu;
        public class OnClickGameMenuEventArgs : EventArgs
        {
            public int index;
        }
        
        [Header("References")]
        [SerializeField] private GameInput.GameInput gameInput;


        [Header("Menu Definitions")]
        [SerializeField] private GameMenuStruct[] gameMenuStructArray;
        
        [Serializable]
        public class GameMenuKeyEvent : UnityEvent <string> {}

        [Header("Menu Events")]
        [SerializeField] private GameMenuKeyEvent gameMenuKeyEvent = new GameMenuKeyEvent();
        
        //public GameMenuUiKeyEvent gameMenuUiKeyEvent { get { return _gameMenuUiKeyEvent; } set { _gameMenuUiKeyEvent = value; } }

        private int _selectedMenuIndex = -1;

        /*
         * START
         */
        private void Start()
        {
            gameInput.GetLeftStickVector2Action().performed += GameInputOnGetLeftStickVector2Action;
            gameInput.GetEnterPressAction().performed += GameInputOnMenuClick;
            gameInput.GetSpacePressAction().performed += GameInputOnMenuClick;
            gameInput.GetRightPadDownPressAction().performed += GameInputOnMenuClick;
            
            OnChangeGameMenu?.Invoke(this, new OnChangeGameMenuEventArgs
            {
                GameMenuStructArray = gameMenuStructArray
            });

            if (gameMenuStructArray.Length > 0)
            {
                SetSelectedMenuIndex(0);
            }
        }
        
        /*
         * GameInputOnMenuClick
         */
        private void GameInputOnMenuClick(InputAction.CallbackContext obj)
        {
            OnClickGameMenu?.Invoke(this, new OnClickGameMenuEventArgs
            {
                index = _selectedMenuIndex
            });
            
            //from UI can input by click button by mouse ????
            gameMenuKeyEvent?.Invoke(gameMenuStructArray[_selectedMenuIndex].menuKey);
        }

        /*
         * SetSelectedMenuIndex
         */
        private void SetSelectedMenuIndex(int index)
        {
            int oldSelectedMenuIndex = _selectedMenuIndex;
            _selectedMenuIndex = index;
            OnSelectGameMenu?.Invoke(this, new OnSelectGameMenuEventArgs
            {
                oldIndex = oldSelectedMenuIndex,
                newIndex = index
            });
        }

        /*
         * GameInputOnGetLeftStickVector2Action
         */
        private void GameInputOnGetLeftStickVector2Action(InputAction.CallbackContext obj)
        {
            Vector2 leftStick = obj.ReadValue<Vector2>();
            int newSelectedMenuIndex = _selectedMenuIndex;
            if (leftStick.y < 0)
            {
                newSelectedMenuIndex++;
                if (newSelectedMenuIndex > gameMenuStructArray.Length - 1)
                {
                    newSelectedMenuIndex = gameMenuStructArray.Length - 1;
                } 
            } else if (leftStick.y > 0)
            {
                newSelectedMenuIndex--;
                if (newSelectedMenuIndex < 0)
                {
                    newSelectedMenuIndex = 0;
                }
            }
            SetSelectedMenuIndex(newSelectedMenuIndex);
        }

        /*
         * OnDestroy
         */
        private void OnDestroy()
        {
            gameInput.GetEnterPressAction().performed -= GameInputOnMenuClick;
            gameInput.GetSpacePressAction().performed -= GameInputOnMenuClick;
            gameInput.GetRightPadDownPressAction().performed -= GameInputOnMenuClick;
            gameInput.GetLeftStickVector2Action().performed -= GameInputOnGetLeftStickVector2Action;
        }
    }
}
