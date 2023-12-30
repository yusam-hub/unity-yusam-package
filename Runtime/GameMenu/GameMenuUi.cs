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
        [SerializeField] private GameMenu gameMenu;
        [SerializeField] private GameMenuItemsUi gameMenuItemsUi;
        [SerializeField] private GameMenuItemUi prefabGameMenuItemUi;
        
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
            if (gameMenu == null)
            {
                Debug.LogError("GameInput instance not found! " + this);
            }
            
            _menuList = new List<GameMenuItemUi>();
            
            gameMenu.OnChangeGameMenu += GameMenuOnChangeGameMenu;
            gameMenu.OnSelectGameMenu += GameMenuOnSelectGameMenu;
        }

        private void GameMenuOnSelectGameMenu(object sender, GameMenu.OnSelectGameMenuEventArgs e)
        {
            SetSelectedMenuIndex(e.oldIndex, e.newIndex);
        }

        private void GameMenuOnChangeGameMenu(object sender, GameMenu.OnChangeGameMenuEventArgs e)
        {
            foreach (GameMenu.GameMenuStruct menuItem in e.GameMenuStructArray)
            {
                GameMenuItemUi gameMenuItemUi = Instantiate(prefabGameMenuItemUi, gameMenuItemsUi.transform);
                gameMenuItemUi.SetMenuIndex(_menuList.Count); 
                gameMenuItemUi.SetMenuKey(menuItem.menuKey); 
                gameMenuItemUi.SetMenuText(menuItem.menuText); 
                //gameMenuItemUi.OnMenuClick += OnMenuClick;
                //gameMenuItemUi.OnMenuEnter += OnMenuEnter;
                _menuList.Add(gameMenuItemUi);
            }
            
            if (_menuList.Count > 0)
            {
                SetSelectedMenuIndex(-1, 0);
            }
        }

        private void SetSelectedMenuIndex(int oldIndex, int newIndex)
        {
            if (oldIndex >= 0 && oldIndex < _menuList.Count)
            {
                _menuList[oldIndex].SetColorDefault();
            }
            
            if (newIndex >= 0 && newIndex < _menuList.Count)
            {
                _selectedMenuIndex = newIndex;
                _menuList[newIndex].SetColorHover();
            }
        }

        private void OnDestroy()
        {
            int c = _menuList.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                GameMenuItemUi gameMenuItemUi = _menuList[i];
                _menuList.RemoveAt(i);
                //gameMenuItemUi.OnMenuClick -= OnMenuClick;
                //gameMenuItemUi.OnMenuEnter -= OnMenuEnter;
                Destroy(gameMenuItemUi);
            }

            //Debug.Log("OnDestroy: " + this.name);
        }
    }
}
