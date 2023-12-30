using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage.GameInput
{
    
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }
        
        [SerializeField] private GameInputUi gameInputUi;
        [SerializeField] private bool showUi = false;
        
        private GameInputActions _gameInputActions;

        /*
         * AWAKE
         */
        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            DontDestroyOnLoad(this);
            
            _gameInputActions = new GameInputActions();
            _gameInputActions.DefaultMap.Enable();
            //_gameInputActions.DefaultMap.AInteractAction.performed += AInteractActionOnPerformed;
            //_gameInputActions.DefaultMap.BInteractAction.performed += BInteractActionOnPerformed;
        }

        /*
         * DESTROY
         */
        private void OnDestroy()
        {
            //_gameInputActions.DefaultMap.AInteractAction.performed -= AInteractActionOnPerformed;
            //_gameInputActions.DefaultMap.BInteractAction.performed -= BInteractActionOnPerformed;
            _gameInputActions.Dispose();
            Debug.Log("OnDestroy: " + this.name);
        }

        /*
         * START
         */
        private void Start()
        {
            gameInputUi.gameObject.SetActive(showUi);
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

        /*
         * UPDATE
         */
        private void Update()
        {
            if (showUi && gameInputUi)
            {
                /*
                 * LEFT STICK
                 */
                Vector2 leftStick = GetLeftStickVector2Normalized();

                gameInputUi.leftStick.leftImage.color = leftStick.x < 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.leftStick.rightImage.color = leftStick.x > 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.leftStick.topImage.color = leftStick.y > 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.leftStick.bottomImage.color = leftStick.y < 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                gameInputUi.leftStick.centerImage.color = _gameInputActions.DefaultMap.LeftStickPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * RIGHT STICK
                 */
                Vector2 rightStick = GetRightStickVector2Normalized();

                gameInputUi.rightStick.leftImage.color = rightStick.x < 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.rightStick.rightImage.color = rightStick.x > 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.rightStick.topImage.color = rightStick.y > 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;

                gameInputUi.rightStick.bottomImage.color = rightStick.y < 0
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                gameInputUi.rightStick.centerImage.color = _gameInputActions.DefaultMap.RightStickPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * LEFT PAD
                 */
                gameInputUi.leftPad.leftImage.color = _gameInputActions.DefaultMap.LeftPadLeftPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.leftPad.rightImage.color = _gameInputActions.DefaultMap.LeftPadRightPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.leftPad.topImage.color = _gameInputActions.DefaultMap.LeftPadUpPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.leftPad.bottomImage.color = _gameInputActions.DefaultMap.LeftPadDownPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * RIGHT PAD
                 */
                gameInputUi.rightPad.leftImage.color = _gameInputActions.DefaultMap.RightPadLeftPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.rightPad.rightImage.color = _gameInputActions.DefaultMap.RightPadRightPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.rightPad.topImage.color = _gameInputActions.DefaultMap.RightPadUpPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.rightPad.bottomImage.color = _gameInputActions.DefaultMap.RightPadDownPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * SELECT START
                 */
                gameInputUi.selectStart.leftImage.color = _gameInputActions.DefaultMap.SelectPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.selectStart.rightImage.color = _gameInputActions.DefaultMap.StartPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * LEFT V
                 */
                gameInputUi.leftV.topImage.color = _gameInputActions.DefaultMap.LeftBumperPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.leftV.bottomImage.color = _gameInputActions.DefaultMap.LeftTriggerPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * RIGHT V
                 */
                gameInputUi.rightV.topImage.color = _gameInputActions.DefaultMap.RightBumperPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.rightV.bottomImage.color = _gameInputActions.DefaultMap.RightTriggerPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                
                /*
                 * MOUSE BUTTON
                 */
                gameInputUi.mouseButton.leftImage.color = _gameInputActions.DefaultMap.MouseLeftPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.mouseButton.centerImage.color = _gameInputActions.DefaultMap.MouseMiddlePress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
                gameInputUi.mouseButton.rightImage.color = _gameInputActions.DefaultMap.MouseRightPress.IsPressed()      
                    ? gameInputUi.selectedColor
                    : gameInputUi.defaultColor;
            }
        }
    }
}
