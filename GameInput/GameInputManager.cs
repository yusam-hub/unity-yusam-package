using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YusamPackage.GameDebug;
using YusamPackage.GameInput.Ui;

namespace YusamPackage.GameInput
{
    public class GameInputManager : MonoBehaviour, IGameInputManager
    {
        public static GameInputManager Instance { get; private set; }

        
        [SerializeField] private GameInputManagerUi gameInputManagerUi;
        [SerializeField] private bool showUi = false;
        
        private GameInputActions _gameInputActions;
        
        /*
         * AWAKE
         */
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

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
        }

        /*
         * START
         */
        private void Start()
        {
            if (gameInputManagerUi)
            {
                gameInputManagerUi.gameObject.SetActive(showUi);
            }
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
        
        /*
         * VECTOR 2
         */
        public Vector2 GetLeftStickVector2Normalized()
        {
            return _gameInputActions.DefaultMap.LeftStickVector2.ReadValue<Vector2>();
        }
        public Vector2 GetRightStickVector2Normalized()
        {
            return _gameInputActions.DefaultMap.RightStickVector2.ReadValue<Vector2>();
        }

        /*
         * UPDATE
         */
        private void Update()
        {
            if (showUi && gameInputManagerUi)
            {
                /*
                 * LEFT STICK
                 */
                Vector2 leftStick = GetLeftStickVector2Normalized();

                gameInputManagerUi.leftStick.leftImage.color = leftStick.x < 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.leftStick.rightImage.color = leftStick.x > 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.leftStick.topImage.color = leftStick.y > 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.leftStick.bottomImage.color = leftStick.y < 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                gameInputManagerUi.leftStick.centerImage.color = _gameInputActions.DefaultMap.LeftStickPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * RIGHT STICK
                 */
                Vector2 rightStick = GetRightStickVector2Normalized();

                gameInputManagerUi.rightStick.leftImage.color = rightStick.x < 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.rightStick.rightImage.color = rightStick.x > 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.rightStick.topImage.color = rightStick.y > 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;

                gameInputManagerUi.rightStick.bottomImage.color = rightStick.y < 0
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                gameInputManagerUi.rightStick.centerImage.color = _gameInputActions.DefaultMap.RightStickPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * LEFT PAD
                 */
                gameInputManagerUi.leftPad.leftImage.color = _gameInputActions.DefaultMap.LeftPadLeftPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.leftPad.rightImage.color = _gameInputActions.DefaultMap.LeftPadRightPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.leftPad.topImage.color = _gameInputActions.DefaultMap.LeftPadUpPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.leftPad.bottomImage.color = _gameInputActions.DefaultMap.LeftPadDownPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * RIGHT PAD
                 */
                gameInputManagerUi.rightPad.leftImage.color = _gameInputActions.DefaultMap.RightPadLeftPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.rightPad.rightImage.color = _gameInputActions.DefaultMap.RightPadRightPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.rightPad.topImage.color = _gameInputActions.DefaultMap.RightPadUpPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.rightPad.bottomImage.color = _gameInputActions.DefaultMap.RightPadDownPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * SELECT START
                 */
                gameInputManagerUi.selectStart.leftImage.color = _gameInputActions.DefaultMap.SelectPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.selectStart.rightImage.color = _gameInputActions.DefaultMap.StartPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * LEFT V
                 */
                gameInputManagerUi.leftV.topImage.color = _gameInputActions.DefaultMap.LeftBumperPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.leftV.bottomImage.color = _gameInputActions.DefaultMap.LeftTriggerPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                
                /*
                 * RIGHT V
                 */
                gameInputManagerUi.rightV.topImage.color = _gameInputActions.DefaultMap.RightBumperPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
                gameInputManagerUi.rightV.bottomImage.color = _gameInputActions.DefaultMap.RightTriggerPress.IsPressed()      
                    ? gameInputManagerUi.selectedColor
                    : gameInputManagerUi.defaultColor;
            }
        }
    }
}
