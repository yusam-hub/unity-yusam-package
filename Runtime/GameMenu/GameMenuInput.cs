using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    public class GameMenuInput : MonoBehaviour
    {
        public event EventHandler<OnGameMenuInputSelectedArgs> OnGameMenuInputSelected;
        public class OnGameMenuInputSelectedArgs : EventArgs
        {
            public int oldIndex;
            public int newIndex;
        }
        
        public event EventHandler<OnGameMenuInputClickedArgs> OnGameMenuInputClicked;
        public class OnGameMenuInputClickedArgs : EventArgs
        {
            public int index;
        }
        
        [SerializeField] private GameInput gameInput;
        [SerializeField] private GameMenuSo gameMenuSo;
        
        private int _selectedMenuIndex = -1;

        private void OnEnable()
        {
            _selectedMenuIndex = -1;
            
            gameInput.GetLeftStickDirectionAction().performed += GameInputOnGetLeftStickVector2Action;
            gameInput.GetSpacePressAction().performed += OnGameInputClicked;
            gameInput.GetRightPadDownPressAction().performed += OnGameInputClicked;
        }

        private void OnGameInputClicked(InputAction.CallbackContext obj)
        {
            OnGameMenuInputClicked?.Invoke(this, new OnGameMenuInputClickedArgs
            {
                index = _selectedMenuIndex
            });
        }

        private void GameInputOnGetLeftStickVector2Action(InputAction.CallbackContext obj)
        {
            int newSelectedMenuIndex = _selectedMenuIndex;

            var leftStick = obj.ReadValue<Vector2>();
            if (leftStick.y < 0)
            {
                newSelectedMenuIndex++;
                if (newSelectedMenuIndex > gameMenuSo.gameMenuStructArray.Length - 1)
                {
                    newSelectedMenuIndex = gameMenuSo.gameMenuStructArray.Length - 1;
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
        
        private void OnDisable()
        {
            gameInput.GetSpacePressAction().performed -= OnGameInputClicked;
            gameInput.GetRightPadDownPressAction().performed -= OnGameInputClicked;
            gameInput.GetLeftStickDirectionAction().performed -= GameInputOnGetLeftStickVector2Action;
        }

        public GameMenuSo.GameMenuStruct[] GetGameMenuStruct()
        {
            return gameMenuSo.gameMenuStructArray;
        }

        public void SetSelectedMenuIndex(int index)
        {
            int oldSelectedMenuIndex = _selectedMenuIndex;
            
            _selectedMenuIndex = index;
            
            OnGameMenuInputSelected?.Invoke(this, new OnGameMenuInputSelectedArgs
            {
                oldIndex = oldSelectedMenuIndex,
                newIndex = index
            });
        }
    }
}
