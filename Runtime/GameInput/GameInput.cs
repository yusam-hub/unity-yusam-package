using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    
    public class GameInput : MonoBehaviour
    {
        private static GameInput Instance { get; set; }

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
            switch (gameInputPerformedEnum)
            {
                case GameInputPerformedEnum.StartPress:
                    return GetStartPressAction();
                case GameInputPerformedEnum.EnterPress:
                    return GetEnterPressAction();
                case GameInputPerformedEnum.RightPadDownPress:
                    return GetRightPadDownPressAction();
            }
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
         * VECTOR 2
         */
        public InputAction GetLeftStickVector2Action()
        {
            return _gameInputActions.DefaultMap.LeftStickVector2;
        }
        public InputAction GetRightStickVector2Action()
        {
            return _gameInputActions.DefaultMap.RightStickVector2;
        }
        
        public Vector2 GetLeftStickVector2Normalized()
        {
            return GetLeftStickVector2Action().ReadValue<Vector2>();
        }
        public Vector2 GetRightStickVector2Normalized()
        {
            return GetRightStickVector2Action().ReadValue<Vector2>();
        }
        
        /*
         * MOUSE
         */
        public Vector2 GetMouseNormalized()
        {
            return _gameInputActions.DefaultMap.MouseVector2.ReadValue<Vector2>();
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
