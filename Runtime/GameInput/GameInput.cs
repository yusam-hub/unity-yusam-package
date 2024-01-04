using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

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
        private bool debugEnabled;
        [SerializeField]
        private Color debugColor = Color.red;  
        [SerializeField] private float virtualCursorSpeed = 1000;
        [SerializeField] private RectTransform cursorRectTransform;
        [SerializeField] private RectTransform canvasRectTransform;

        public static GameInput Instance { get; private set; }

        private YusamPackageGameInputActions _gameInputActions;
        private Canvas _canvas;
        private Camera _camera;
        private Mouse _virtualMouse;
        /*
         * AWAKE
         */
        private void Awake()
        {
            _camera = Camera.main;
            
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _gameInputActions = new YusamPackageGameInputActions();
            _gameInputActions.DefaultMap.Enable();
            
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            
            if (canvasRectTransform != null)
            {
                _canvas = canvasRectTransform.GetComponent<Canvas>();
            }
 
            if (cursorRectTransform != null && _virtualMouse != null)
            {
                cursorRectTransform.gameObject.SetActive(true);
                InputState.Change(_virtualMouse.position, cursorRectTransform.anchoredPosition);
            }
            
            InputSystem.onAfterUpdate += InputSystemOnAfterUpdate;
        }

        private void InputSystemOnAfterUpdate()
        {
            if (_virtualMouse == null && Gamepad.current == null)
            {
                return;
            }
            
            Vector2 deltaValue = GetRightStickDirection();
            deltaValue *= virtualCursorSpeed * Time.deltaTime;

            Vector2 currentPosition = _virtualMouse.position.ReadValue();
            Vector2 newPosition = currentPosition + deltaValue;

            newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
            newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);
        
            InputState.Change(_virtualMouse.position, newPosition);
            InputState.Change(_virtualMouse.delta, deltaValue);

            /*bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
            if (_previousMouseState != aButtonIsPressed)
            {
                _virtualMouse.CopyState<MouseState>(out var mouseState);
                mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
                InputState.Change(_virtualMouse, mouseState);
                _previousMouseState = aButtonIsPressed;
            }*/
            
            AnchorCursor(newPosition);
        }
        
        private void AnchorCursor(Vector2 position)
        {
            if (cursorRectTransform == null) return;
            
            Vector2 anchoredPosition = position;

            if (canvasRectTransform != null && _canvas != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRectTransform,
                    position,
                    _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _camera, 
                    out anchoredPosition
                );
            }
        
            cursorRectTransform.anchoredPosition = anchoredPosition;        
        }
        
        /*
         * DESTROY
         */
        private void OnDestroy()
        {
            InputSystem.onAfterUpdate -= InputSystemOnAfterUpdate;

            InputSystem.RemoveDevice(_virtualMouse);
            
            _gameInputActions.Dispose();
        }

        //GetActionByEnum
        public InputAction GetActionByEnum(GameInputPerformedEnum gameInputPerformedEnum)
        {
            switch (gameInputPerformedEnum)
            {
                case GameInputPerformedEnum.MouseLeftPress:
                    return GetMouseLeftPressAction();
                case GameInputPerformedEnum.MouseMiddlePress:
                    return GetMouseMiddlePressAction();
                case GameInputPerformedEnum.MouseRightPress:
                    return GetMouseRightPressAction();
                
                case GameInputPerformedEnum.EscapePress:
                    return GetEscapePressAction();
                case GameInputPerformedEnum.EnterPress:
                    return GetEnterPressAction();
                case GameInputPerformedEnum.SpacePress:
                    return GetSpacePressAction();

                case GameInputPerformedEnum.StartPress:
                    return GetStartPressAction();
                case GameInputPerformedEnum.SelectPress:
                    return GetSelectPressAction();
                
                case GameInputPerformedEnum.RightPadUpPress:
                    return GetRightPadUpPressAction();
                case GameInputPerformedEnum.RightPadDownPress:
                    return GetRightPadDownPressAction();
                case GameInputPerformedEnum.RightPadLeftPress:
                    return GetRightPadLeftPressAction();
                case GameInputPerformedEnum.RightPadRightPress:
                    return GetRightPadRightPressAction();      
                
                case GameInputPerformedEnum.LeftPadUpPress:
                    return GetLeftPadUpPressAction();
                case GameInputPerformedEnum.LeftPadDownPress:
                    return GetLeftPadDownPressAction();
                case GameInputPerformedEnum.LeftPadLeftPress:
                    return GetLeftPadLeftPressAction();
                case GameInputPerformedEnum.LeftPadRightPress:
                    return GetLeftPadRightPressAction();   
                
                case GameInputPerformedEnum.LeftBumperPress:
                    return GetLeftBumperPressAction();
                case GameInputPerformedEnum.LeftTriggerPress:
                    return GetLeftTriggerPressAction();
                case GameInputPerformedEnum.RightBumperPress:
                    return GetRightBumperPressAction();
                case GameInputPerformedEnum.RightTriggerPress:
                    return GetRightTriggerPressAction();    
                
                case GameInputPerformedEnum.LeftStickPress:
                    return GetLeftStickPressAction();  
                case GameInputPerformedEnum.RightStickPress:
                    return GetRightStickPressAction();       
                
                case GameInputPerformedEnum.BothPrimaryPress:
                    return GetBothPrimaryPressAction();  
                case GameInputPerformedEnum.BothSecondaryPress:
                    return GetBothSecondaryPressAction();     
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
        
        public InputAction GetVirtualMousePositionAction()
        {
            return _gameInputActions.DefaultMap.VirtualMousePosition;
        }
        
        public Vector2 GetVirtualMousePosition()
        {
            return GetVirtualMousePositionAction().ReadValue<Vector2>();
        }
        
        public InputAction GetBothMousePositionAction()
        {
            return _gameInputActions.DefaultMap.BothMousePosition;
        }
        
        public Vector2 GetBothMousePosition()
        {
            return GetBothMousePositionAction().ReadValue<Vector2>();
        }
        
        /*
         * MOUSE
         */
        public InputAction GetMouseDirectionAction()
        {
            return _gameInputActions.DefaultMap.MouseDirection;
        }
        
        public Vector2 GetMouseDirection()
        {
            return GetMouseDirectionAction().ReadValue<Vector2>();
        }
        
        public InputAction GetMousePositionAction()
        {
            return _gameInputActions.DefaultMap.MousePosition;
        }
        
        public Vector2 GetMousePosition()
        {
            return GetMousePositionAction().ReadValue<Vector2>();
        }
        
        public InputAction GetMouseScrollAction()
        {
            return _gameInputActions.DefaultMap.MouseScroll;
        }
        
        public Vector2 GetMouseScroll()
        {
            return GetMouseScrollAction().ReadValue<Vector2>();
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
        
        public InputAction GetBothPrimaryPressAction()
        {
            return _gameInputActions.DefaultMap.BothPrimaryPress;
        }
        
        public InputAction GetBothSecondaryPressAction()
        {
            return _gameInputActions.DefaultMap.BothSecondaryPress;
        }

        public Vector3 GetLeftStickDirectionAsVector3()
        {
            Vector2 leftStickDirection = GetLeftStickDirection();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        
        void OnGUI()
        {
            if (!debugEnabled) return;            
            GUILayout.BeginArea(new Rect(0,0, Screen.width, Screen.height));
            
            GUIStyle style = GUI.skin.label;
            style.normal.textColor = debugColor;
            
            GUILayout.Label("Mouse Position: " + GetMousePosition(), style);
            GUILayout.Label("Virtual Mouse Position: " + GetVirtualMousePosition(), style);           
            GUILayout.Label("Both Mouse Position: " + GetBothMousePosition(), style);           

            GUILayout.EndArea();
        }
    }
}
