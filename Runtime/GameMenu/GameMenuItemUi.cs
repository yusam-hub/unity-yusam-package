using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YusamPackage
{
    public class GameMenuItemUi : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        public class OnMenuEventArgs : EventArgs
        {
            public int menuIndex;
            public string menuKey;
        }
        
        public EventHandler<OnMenuEventArgs> OnMenuClick;
        public EventHandler<OnMenuEventArgs> OnMenuEnter;
        public EventHandler<OnMenuEventArgs> OnMenuExit;

        
        [Header("References")]
        [SerializeField] private Image menuBackground;
        [SerializeField] private Button menuButton;
        [SerializeField] private Text menuText;
        
        [Header("Border Colors")]
        [SerializeField] private Color defaultBorderColor = Color.white;
        [SerializeField] private Color hoverBorderColor = Color.gray;
        
        [Header("Button Colors")]
        [SerializeField] private Color defaultButtonColor = Color.gray;
        [SerializeField] private Color hoverButtonColor = Color.white;
        
        [Header("Text Colors")]
        [SerializeField] private Color defaultTextColor = Color.white;
        [SerializeField] private Color hoverTextColor = Color.black;

        private string _menuKey;
        private int _menuIndex;
        private void Awake()
        {
            SetColorDefault();
            
            menuButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            menuButton.onClick.RemoveListener(OnButtonClick);
        }
        private void OnButtonClick()
        {
            DoMenuClick();
        }
        
        public void DoMenuClick()
        {
            OnMenuClick?.Invoke(this, new OnMenuEventArgs
            {
                menuIndex = _menuIndex,
                menuKey = _menuKey
            });
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMenuEnter?.Invoke(this, new OnMenuEventArgs
            {
                menuIndex = _menuIndex,
                menuKey = _menuKey
            });
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMenuExit?.Invoke(this, new OnMenuEventArgs
            {
                menuIndex = _menuIndex,
                menuKey = _menuKey
            });
        }

        public void SetColorHover()
        {
            menuBackground.color = hoverBorderColor;
            menuButton.image.color = hoverButtonColor;
            menuText.color = hoverTextColor;
        }
        
        public void SetColorDefault()
        {
            menuBackground.color = defaultBorderColor;
            menuButton.image.color = defaultButtonColor;
            menuText.color = defaultTextColor;
        }

        public void SetMenuKey(string key)
        {
            _menuKey = key;
        }

        public string GetMenuKey()
        {
            return _menuKey;
        }
        
        public void SetMenuIndex(int index)
        {
            _menuIndex = index;
        }

        public int GetMenuIndex()
        {
            return _menuIndex;
        }
        
        public void SetMenuText(string text)
        {
            menuText.text = text;
        }
    }
}