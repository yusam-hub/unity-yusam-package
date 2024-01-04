using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class GameMenuUi : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameMenu gameMenu;
        [SerializeField] private GameMenuItemsUi gameMenuItemsUi;
        [SerializeField] private GameMenuItemUi prefabGameMenuItemUi;
        
        private List<GameMenuItemUi> _menuList;
        
        //Awake
        private void Awake()
        {
            Debug.Log($"{GetType()} - Awake");
            
            if (gameMenu == null)
            {
                Debug.LogError("GameInput instance not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            
            _menuList = new List<GameMenuItemUi>();
            
            gameMenu.OnChangeGameMenu += GameMenuOnChangeGameMenu;
            gameMenu.OnSelectGameMenu += GameMenuOnSelectGameMenu;
        }

        private void OnEnable()
        {
            RefreshMenuUi();
        }

        private void RefreshMenuUi()
        {
            ClearMenuItemUi();
            
            foreach (GameMenuSo.GameMenuStruct menuItem in gameMenu.GetGameMenuStruct())
            {
                GameMenuItemUi gameMenuItemUi = Instantiate(prefabGameMenuItemUi, gameMenuItemsUi.transform);
                gameMenuItemUi.SetMenuIndex(_menuList.Count); 
                gameMenuItemUi.SetMenuKey(menuItem.menuKey); 
                gameMenuItemUi.SetMenuText(menuItem.menuText); 
                gameMenuItemUi.OnMenuClick += GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter += GameMenuItemUiOnMenuEnter;
                gameMenuItemUi.OnMenuExit += GameMenuItemUiOnMenuExit;
                _menuList.Add(gameMenuItemUi);
            }
            
            if (_menuList.Count > 0)
            {
                gameMenu.SetSelectedMenuIndex(0);
            }
        }
        
        //обработчик изменения главного меню
        private void GameMenuOnChangeGameMenu(object sender, EventArgs e)
        {
            RefreshMenuUi();
        }
        
        //ClearMenuItemUi
        private void ClearMenuItemUi()
        {
            int c = _menuList.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                GameMenuItemUi gameMenuItemUi = _menuList[i];
                
                //Debug.Log("Delete: " + gameMenuItemUi.name);
                
                _menuList.RemoveAt(i);
                gameMenuItemUi.OnMenuClick -= GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter -= GameMenuItemUiOnMenuEnter;
                gameMenuItemUi.OnMenuExit -= GameMenuItemUiOnMenuExit;
                Destroy(gameMenuItemUi.gameObject);
            }
        }

        //OnDestroy
        private void OnDestroy()
        {
            gameMenu.OnChangeGameMenu -= GameMenuOnChangeGameMenu;
            gameMenu.OnSelectGameMenu -= GameMenuOnSelectGameMenu;

            ClearMenuItemUi();

            Debug.Log($"{GetType()} - OnDestroy");
        }

        /**
         * изменияем цвета от выбраного меню из главного обработчика
         */
        private void GameMenuOnSelectGameMenu(object sender, GameMenu.OnSelectGameMenuEventArgs e)
        {
            if (e.oldIndex >= 0 && e.oldIndex < _menuList.Count)
            {
                _menuList[e.oldIndex].SetColorDefault();
            }
            
            if (e.newIndex >= 0 && e.newIndex < _menuList.Count)
            {
                _menuList[e.newIndex].SetColorHover();
            }
        }

        

        //сообщаем главному управлению что мы мышкой зашли в объект
        private void GameMenuItemUiOnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            gameMenu.SetSelectedMenuIndex(e.menuIndex);
        }

        //уведомляем о том, что мы мышкой нажали на кнопку
        private void GameMenuItemUiOnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            gameMenu.OnGameMenuKeyEvent?.Invoke(e.menuKey);
        }
        
        //если мышка то убираем выбор
        private void GameMenuItemUiOnMenuExit(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            _menuList[e.menuIndex].SetColorDefault();
        }
        
    }
}
