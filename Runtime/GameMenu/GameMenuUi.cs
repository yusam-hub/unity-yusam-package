using System.Collections.Generic;
using UnityEngine;

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
            
            foreach (var menuItem in _gameMenuInput.GetGameMenuStruct())
            {
                var gameMenuItemUi = Instantiate(prefabGameMenuItemUi, gameMenuItemsUi.transform);
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
            var c = _menuList.Count;
            for (var i = c - 1; i >= 0; i--)
            {
                var  gameMenuItemUi = _menuList[i];
                _menuList.RemoveAt(i);
                gameMenuItemUi.OnMenuClick -= GameMenuItemUiOnMenuClick;
                gameMenuItemUi.OnMenuEnter -= GameMenuItemUiOnMenuEnter;
                gameMenuItemUi.OnMenuExit -= GameMenuItemUiOnMenuExit;
                Destroy(gameMenuItemUi.gameObject);
            }
        }

        private void OnGameMenuInputSelected(object sender, GameMenuInput.OnGameMenuInputSelectedArgs e)
        {
            if (e.OldIndex >= 0 && e.OldIndex < _menuList.Count)
            {
                _menuList[e.OldIndex].SetColorDefault();
            }
            
            if (e.NewIndex >= 0 && e.NewIndex < _menuList.Count)
            {
                _menuList[e.NewIndex].SetColorHover();
            }
        }

        private void GameMenuItemUiOnMenuEnter(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            _gameMenuInput.SetSelectedMenuIndex(e.MenuIndex);
        }

        private void OnGameMenuInputClicked(object sender, GameMenuInput.OnGameMenuInputClickedArgs e)
        {
            onClickedEvent?.Invoke(gameObject, _gameMenuInput.GetGameMenuStruct()[e.Index].menuKey);
        }
        
        private void GameMenuItemUiOnMenuClick(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            onClickedEvent?.Invoke(gameObject, e.MenuKey);
        }
        
        private void GameMenuItemUiOnMenuExit(object sender, GameMenuItemUi.OnMenuEventArgs e)
        {
            _menuList[e.MenuIndex].SetColorDefault();
        }
    }
}
