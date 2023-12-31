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
            Debug.Log("Awake: " + this.name);
            
            if (gameMenu == null)
            {
                Debug.LogError("GameInput instance not found! " + this);
            }
            
            _menuList = new List<GameMenuItemUi>();
            
            gameMenu.OnChangeGameMenu += GameMenuOnChangeGameMenu;
            gameMenu.OnSelectGameMenu += GameMenuOnSelectGameMenu;
        }


        private void OnDestroy()
        {
            gameMenu.OnChangeGameMenu -= GameMenuOnChangeGameMenu;
            gameMenu.OnSelectGameMenu -= GameMenuOnSelectGameMenu;

            int c = _menuList.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                GameMenuItemUi gameMenuItemUi = _menuList[i];
                _menuList.RemoveAt(i);
                gameMenuItemUi.OnMenuClick -= GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter -= GameMenuItemUiOnMenuEnter;
                Destroy(gameMenuItemUi);
            }

            Debug.Log("OnDestroy: " + this.name);
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
                gameMenuItemUi.OnMenuClick += GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter += GameMenuItemUiOnMenuEnter;
                _menuList.Add(gameMenuItemUi);
            }
            
            if (_menuList.Count > 0)
            {
                SetSelectedMenuIndex(-1, 0);
            }
        }

        private void GameMenuItemUiOnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            SetSelectedMenuIndex(_selectedMenuIndex, e.menuIndex);
        }

        private void GameMenuItemUiOnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            gameMenu.OnGameMenuKeyEvent?.Invoke(e.menuKey);
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


    }
}
