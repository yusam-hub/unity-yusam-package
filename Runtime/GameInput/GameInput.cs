using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    
    [DisallowMultipleComponent]
    public class GameInput : MonoBehaviour
    {
        [Space(10)]
        [YusamHelpBox("GameInput - контроллер управления, быть статичным статичным и один на сцене")]
        [Space(10)]
        [YusamHelpBox("Project Settings -> Player -> Active Input Handling = Both | Input System Package (New)")]
        [Space(10)]
        [SerializeField]
        private string desc;
        
        public static GameInput Instance { get; private set; }

        private GameInputActions _gameInputActions;
        
        /*
         * AWAKE
         */
        private void Awake()
        {
            GameDebug.Log("Awake: " + this.name);
            
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _gameInputActions = new GameInputActions();
            _gameInputActions.DefaultMap.Enable();
        }

        /*
         * DESTROY
         */
        private void OnDestroy()
        {
            _gameInputActions.Dispose();
            GameDebug.Log("OnDestroy: " + this.name);
        }

        //GetActionByEnum
        public InputAction GetActionByEnum(GameInputPerformedEnum gameInputPerformedEnum)
        {
            //todo: all
            switch (gameInputPerformedEnum)
            {
                case GameInputPerformedEnum.MouseLeftPress:
                    return GetMouseLeftPressAction();
                case GameInputPerformedEnum.MouseMiddlePress:
                    return GetMouseMiddlePressAction();
                case GameInputPerformedEnum.MouseRightPress:
                    return GetMouseRightPressAction();
                case GameInputPerformedEnum.SpacePress:
                    return GetSpacePressAction();
                case GameInputPerformedEnum.StartPress:
                    return GetStartPressAction();
                case GameInputPerformedEnum.EnterPress:
                    return GetEnterPressAction();
                case GameInputPerformedEnum.RightPadDownPress:
                    return GetRightPadDownPressAction();
            }
            Debug.LogError($"{gameInputPerformedEnum} is not implemented");
            return null;
        }
        
        /*
         * PRESS
         */
        public InputAction GetLeftTriggerPressAction()
        {
            return _gameInputActions.DefaultMap.LeftTriggerPress;
        }
        public InputAction GetLeftBumperPressAction()
        {
            return _gameInputActions.DefaultMap.LeftBumperPress;
        }
        public InputAction GetLeftStickPressAction()
        {
            return _gameInputActions.DefaultMap.LeftStickPress;
        }
        public InputAction GetLeftPadLeftPressAction()
        {
            return _gameInputActions.DefaultMap.LeftPadLeftPress;
        }
        public InputAction GetLeftPadRightPressAction()
        {
            return _gameInputActions.DefaultMap.LeftPadRightPress;
        }
        public InputAction GetLeftPadUpPressAction()
        {
            return _gameInputActions.DefaultMap.LeftPadUpPress;
        }
        public InputAction GetLeftPadDownPressAction()
        {
            return _gameInputActions.DefaultMap.LeftPadDownPress;
        }
        public InputAction GetStartPressAction()
        {
            return _gameInputActions.DefaultMap.StartPress;
        }
        public InputAction GetSelectPressAction()
        {
            return _gameInputActions.DefaultMap.SelectPress;
        }
        public InputAction GetRightPadLeftPressAction()
        {
            return _gameInputActions.DefaultMap.RightPadLeftPress;
        }
        public InputAction GetRightPadRightPressAction()
        {
            return _gameInputActions.DefaultMap.RightPadRightPress;
        }
        public InputAction GetRightPadUpPressAction()
        {
            return _gameInputActions.DefaultMap.RightPadUpPress;
        }
        public InputAction GetRightPadDownPressAction()
        {
            return _gameInputActions.DefaultMap.RightPadDownPress;
        }
        public InputAction GetRightStickPressAction()
        {
            return _gameInputActions.DefaultMap.RightStickPress;
        }
        public InputAction GetRightTriggerPressAction()
        {
            return _gameInputActions.DefaultMap.RightTriggerPress;
        }
        public InputAction GetRightBumperPressAction()
        {
            return _gameInputActions.DefaultMap.RightBumperPress;
        }
        public InputAction GetEnterPressAction()
        {
            return _gameInputActions.DefaultMap.EnterPress;
        }
        public InputAction GetEscapePressAction()
        {
            return _gameInputActions.DefaultMap.EscapePress;
        }
        public InputAction GetSpacePressAction()
        {
            return _gameInputActions.DefaultMap.SpacePress;
        }
        
        /*
         * STICK
         */
        public InputAction GetLeftStickDirectionAction()
        {
            return _gameInputActions.DefaultMap.LeftStickDirection;
        }
        public InputAction GetRightStickDirectionAction()
        {
            return _gameInputActions.DefaultMap.RightStickDirection;
        }
        
        public Vector2 GetLeftStickDirection()
        {
            return GetLeftStickDirectionAction().ReadValue<Vector2>();
        }
        public Vector2 GetRightStickDirection()
        {
            return GetRightStickDirectionAction().ReadValue<Vector2>();
        }
        
        /*
         * MOUSE
         */
        public Vector2 GetMouseDirection()
        {
            return _gameInputActions.DefaultMap.MouseDirection.ReadValue<Vector2>();
        }
        public Vector2 GetMousePosition()
        {
            return _gameInputActions.DefaultMap.MousePosition.ReadValue<Vector2>();
        }
        public InputAction GetMouseLeftPressAction()
        {
            return _gameInputActions.DefaultMap.MouseLeftPress;
        }
        public InputAction GetMouseMiddlePressAction()
        {
            return _gameInputActions.DefaultMap.MouseMiddlePress;
        }
        public InputAction GetMouseRightPressAction()
        {
            return _gameInputActions.DefaultMap.MouseRightPress;
        }

    }
}
