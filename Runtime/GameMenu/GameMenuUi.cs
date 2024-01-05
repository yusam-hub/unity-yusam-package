using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    [RequireComponent(typeof(GameMenuInput))]
    public class GameMenuUi : MonoBehaviour
    {
        [SerializeField] private GameMenuItemsUi gameMenuItemsUi;
        [SerializeField] private GameMenuItemUi prefabGameMenuItemUi;
        [Space(10)]
        [SerializeField] private GameObjectUnityEvent onEnabledEvent = new();
        [SerializeField] private GameObjectStringUnityEvent onClickedEvent = new();
        [SerializeField] private GameObjectUnityEvent onDisabledEvent = new();


        private GameMenuInput _gameMenuInput;
        private List<GameMenuItemUi> _menuList;
        
        private void Awake()
        {
            _gameMenuInput = GetComponent<GameMenuInput>();
            _menuList = new List<GameMenuItemUi>();
        }

        private void OnEnable()
        {
            _gameMenuInput.OnGameMenuInputSelected += OnGameMenuInputSelected;
            _gameMenuInput.OnGameMenuInputClicked += OnGameMenuInputClicked;
            
            RefreshMenuUi();
            
            onEnabledEvent?.Invoke(gameObject);
        }

        private void OnDisable()
        {
            onDisabledEvent?.Invoke(gameObject);
            
            _gameMenuInput.OnGameMenuInputSelected -= OnGameMenuInputSelected;
            _gameMenuInput.OnGameMenuInputClicked -= OnGameMenuInputClicked;
            
            ClearMenuItemUi();
        }

        private void RefreshMenuUi()
        {
            ClearMenuItemUi();
            
            foreach (GameMenuSo.GameMenuStruct menuItem in _gameMenuInput.GetGameMenuStruct())
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
                _gameMenuInput.SetSelectedMenuIndex(0);
            }
        }

        private void ClearMenuItemUi()
        {
            int c = _menuList.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                GameMenuItemUi gameMenuItemUi = _menuList[i];
                _menuList.RemoveAt(i);
                gameMenuItemUi.OnMenuClick -= GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter -= GameMenuItemUiOnMenuEnter;
                gameMenuItemUi.OnMenuExit -= GameMenuItemUiOnMenuExit;
                Destroy(gameMenuItemUi.gameObject);
            }
        }

        private void OnGameMenuInputSelected(object sender, GameMenuInput.OnGameMenuInputSelectedArgs e)
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

        private void GameMenuItemUiOnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            _gameMenuInput.SetSelectedMenuIndex(e.menuIndex);
        }

        private void OnGameMenuInputClicked(object sender, GameMenuInput.OnGameMenuInputClickedArgs e)
        {
            string menuKey = _gameMenuInput.GetGameMenuStruct()[e.index].menuKey;
            onClickedEvent?.Invoke(gameObject, menuKey);
        }
        
        private void GameMenuItemUiOnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            onClickedEvent?.Invoke(gameObject, e.menuKey);
        }
        
        private void GameMenuItemUiOnMenuExit(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            _menuList[e.menuIndex].SetColorDefault();
        }
    }
}
