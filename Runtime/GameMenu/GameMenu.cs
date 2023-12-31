using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    public class GameMenu : MonoBehaviour
    {
        public event EventHandler OnChangeGameMenu;
        
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
        [SerializeField] private GameInput gameInput;

        [SerializeField] private GameMenuSo _gameMenuSo;

        public GameMenuSo gameMenuSo
        {
            get
            {
                return _gameMenuSo;
            }
            set
            {
                _gameMenuSo = value;
                DoGameMenuSoChanged(_gameMenuSo);
            }
        }

        [Serializable]
        public class GameMenuKeyEvent : UnityEvent <string> {}

        [Header("Events")]
        [SerializeField] private GameMenuKeyEvent gameMenuKeyEvent = new GameMenuKeyEvent();
        
        public GameMenuKeyEvent OnGameMenuKeyEvent { get { return gameMenuKeyEvent; } set { gameMenuKeyEvent = value; } }

        private GameMenuSo _lastGameMenuSo;
        private int _selectedMenuIndex = -1;

        //Awake
        private void Awake()
        {
            GameDebug.Log("Awake: " + this.name);
        }

        //Start
        private void Start()
        {
            GameDebug.Log("Start: " + name);
            DoGameMenuSoChanged(_gameMenuSo);
        }

        private void OnEnable()
        {
            GameDebug.Log("OnEnable: " + name);
            
            gameInput.GetLeftStickVector2Action().performed += GameInputOnGetLeftStickVector2Action;
            gameInput.GetEnterPressAction().performed += GameInputOnMenuClick;
            gameInput.GetSpacePressAction().performed += GameInputOnMenuClick;
            gameInput.GetRightPadDownPressAction().performed += GameInputOnMenuClick;
        }

        private void OnDisable()
        {
            gameInput.GetEnterPressAction().performed -= GameInputOnMenuClick;
            gameInput.GetSpacePressAction().performed -= GameInputOnMenuClick;
            gameInput.GetRightPadDownPressAction().performed -= GameInputOnMenuClick;
            gameInput.GetLeftStickVector2Action().performed -= GameInputOnGetLeftStickVector2Action;
            
            GameDebug.Log("OnDisable: " + name); 
        }

        //OnDestroy
        private void OnDestroy()
        {
            GameDebug.Log("OnDestroy: " + name);
        }

        private void Update()
        {
            if (_lastGameMenuSo != _gameMenuSo)
            {
                DoGameMenuSoChanged(_gameMenuSo);
            }
        }

        //DoGameMenuSoChanged
        private void DoGameMenuSoChanged(GameMenuSo newGameMenuSo)
        {
            GameDebug.Log("DoGameMenuSoChanged: " + name);
            
            _lastGameMenuSo = newGameMenuSo;

            OnChangeGameMenu?.Invoke(this, EventArgs.Empty);

            if (_lastGameMenuSo.gameMenuStructArray.Length > 0)
            {
                SetSelectedMenuIndex(0);
            }
        }
        
        //событие на нажатие клавиш
        private void GameInputOnMenuClick(InputAction.CallbackContext obj)
        {
            OnClickGameMenu?.Invoke(this, new OnClickGameMenuEventArgs
            {
                index = _selectedMenuIndex
            });
            
            gameMenuKeyEvent?.Invoke(_lastGameMenuSo.gameMenuStructArray[_selectedMenuIndex].menuKey);
        }

        //возвращаем список меню
        public GameMenuSo.GameMenuStruct[] GetGameMenuStruct()
        {
            return _gameMenuSo.gameMenuStructArray;
        }

        //меняем индекс выбранного меню
        public void SetSelectedMenuIndex(int index)
        {
            int oldSelectedMenuIndex = _selectedMenuIndex;
            
            _selectedMenuIndex = index;
            
            OnSelectGameMenu?.Invoke(this, new OnSelectGameMenuEventArgs
            {
                oldIndex = oldSelectedMenuIndex,
                newIndex = index
            });
        }

        //обрабатываем нажатие клавиш и джостика
        private void GameInputOnGetLeftStickVector2Action(InputAction.CallbackContext obj)
        {
            Vector2 leftStick = obj.ReadValue<Vector2>();
            int newSelectedMenuIndex = _selectedMenuIndex;
            if (leftStick.y < 0)
            {
                newSelectedMenuIndex++;
                if (newSelectedMenuIndex > _lastGameMenuSo.gameMenuStructArray.Length - 1)
                {
                    newSelectedMenuIndex = _lastGameMenuSo.gameMenuStructArray.Length - 1;
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


    }
}
