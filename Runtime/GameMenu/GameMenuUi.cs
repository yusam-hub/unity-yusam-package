using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace YusamPackage.GameMenu
{
    public class GameMenuUi : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private GameMenuItemsUi gameMenuItemsUi;
        [SerializeField] private GameMenuItemUi prefabGameMenuItemUi;
        
        [Serializable]
        public struct GameMenuItemsUiStruct
        {
            public string menuKey;
            public string menuText;
        }
        [Header("Menu Definitions")]
        [SerializeField] private GameMenuItemsUiStruct[] menuArray;


        
        [Serializable]
        public class GameMenuUiKeyEvent : UnityEvent <string> {}

        [Header("Menu Events")]
        [SerializeField]
        private GameMenuUiKeyEvent _gameMenuUiKeyEvent = new GameMenuUiKeyEvent();
        
        //public GameMenuUiKeyEvent gameMenuUiKeyEvent { get { return _gameMenuUiKeyEvent; } set { _gameMenuUiKeyEvent = value; } }

 
        /*
         * PRIVATE
         */
        private List<GameMenuItemUi> _menuList;
        private int _selectedMenuIndex = -1;
        
        /*
         * AWAKE
         */
        private void Awake()
        {
            if (GameInput.GameInput.Instance == null)
            {
                Debug.LogError("GameInput instance not found! " + this);
            }
            
            _menuList = new List<GameMenuItemUi>();
            
            foreach (GameMenuItemsUiStruct menuItem in menuArray)
            {
                GameMenuItemUi gameMenuItemUi = Instantiate(prefabGameMenuItemUi, gameMenuItemsUi.transform);
                gameMenuItemUi.SetMenuIndex(_menuList.Count); 
                gameMenuItemUi.SetMenuKey(menuItem.menuKey); 
                gameMenuItemUi.SetMenuText(menuItem.menuText); 
                gameMenuItemUi.OnMenuClick += OnMenuClick;
                gameMenuItemUi.OnMenuEnter += OnMenuEnter;
                gameMenuItemUi.OnMenuExit += OnMenuExit;
                _menuList.Add(gameMenuItemUi);
            }

            if (_menuList.Count > 0)
            {
                SetSelectedMenuIndex(0);
            }
        }

        private void SetSelectedMenuIndex(int index)
        {
            _selectedMenuIndex = index;
            _menuList[_selectedMenuIndex].SetColorHover();
        }
        
        public string GetSelectedMenuKey()
        {
            return _menuList[_selectedMenuIndex].GetMenuKey();
        }
        
        private void OnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            //Debug.Log("OnMenuClick: " + e.menuKey);
            _menuList[_selectedMenuIndex].SetColorDefault();
            SetSelectedMenuIndex(e.menuIndex);
            _gameMenuUiKeyEvent.Invoke(e.menuKey);
        }

        private void OnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            //Debug.Log("OnMenuEnter: " + e.menuKey);
            _menuList[_selectedMenuIndex].SetColorDefault();
            SetSelectedMenuIndex(e.menuIndex);
        }
        
        private void OnMenuExit(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            //Debug.Log("OnMenuExit: " + e.menuKey);
        }

        /*
         * START
         */
        private void Start()
        {
            if (GameInput.GameInput.Instance)
            {
                GameInput.GameInput.Instance.GetLeftStickVector2Action().performed += OnGetLeftStickVector2Action;
                GameInput.GameInput.Instance.GetEnterPressAction().performed += OnMenuClick;
                GameInput.GameInput.Instance.GetSpacePressAction().performed += OnMenuClick;
                GameInput.GameInput.Instance.GetRightPadDownPressAction().performed += OnMenuClick;
            }
        }

        private void OnMenuClick(InputAction.CallbackContext obj)
        {
            _menuList[_selectedMenuIndex].DoMenuClick();
        }

        private void OnGetLeftStickVector2Action(InputAction.CallbackContext obj)
        {
            Vector2 leftStick = obj.ReadValue<Vector2>();
            _menuList[_selectedMenuIndex].SetColorDefault();
            if (leftStick.y < 0)
            {
                _selectedMenuIndex++;
                if (_selectedMenuIndex > _menuList.Count - 1)
                {
                    _selectedMenuIndex = _menuList.Count - 1;
                } 
            } else if (leftStick.y > 0)
            {
                _selectedMenuIndex--;
                if (_selectedMenuIndex < 0)
                {
                    _selectedMenuIndex = 0;
                }
            }
            SetSelectedMenuIndex(_selectedMenuIndex);
        }

        private void OnDestroy()
        {
            int c = _menuList.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                GameMenuItemUi gameMenuItemUi = _menuList[i];
                _menuList.RemoveAt(i);
                gameMenuItemUi.OnMenuClick -= OnMenuClick;
                gameMenuItemUi.OnMenuEnter -= OnMenuEnter;
                gameMenuItemUi.OnMenuExit -= OnMenuExit;
                Destroy(gameMenuItemUi);
            }

            if (GameInput.GameInput.Instance)
            {
                GameInput.GameInput.Instance.GetEnterPressAction().performed -= OnMenuClick;
                GameInput.GameInput.Instance.GetSpacePressAction().performed -= OnMenuClick;
                GameInput.GameInput.Instance.GetRightPadDownPressAction().performed -= OnMenuClick;
                GameInput.GameInput.Instance.GetLeftStickVector2Action().performed -= OnGetLeftStickVector2Action;
            }

            Debug.Log("OnDestroy: " + this.name);
        }
    }
}
