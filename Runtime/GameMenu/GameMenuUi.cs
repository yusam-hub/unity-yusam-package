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

        /**
         * GameMenuOnSelectGameMenu
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

        private void GameMenuOnChangeGameMenu(object sender, GameMenu.OnChangeGameMenuEventArgs e)
        {
            foreach (GameMenuSo.GameMenuStruct menuItem in e.GameMenuStructArray)
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
                gameMenu.SetSelectedMenuIndex(0);
            }
        }

        private void GameMenuItemUiOnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            gameMenu.SetSelectedMenuIndex(e.menuIndex);
        }

        private void GameMenuItemUiOnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            gameMenu.OnGameMenuKeyEvent?.Invoke(e.menuKey);
        }
    }
}
