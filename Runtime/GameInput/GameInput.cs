using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInput : MonoBehaviour
    {
        [SerializeField] private GameInputCursor gameInputCursor;
        [SerializeField] private float virtualCursorSpeed = 1000;
        [SerializeField] private bool debugInDisplay = false;

        public static GameInput Instance { get; private set; }

        private YusamPackageGameInputActions _gameInputActions;
        private Mouse _virtualMouse;
        private Camera _camera;
        
        /*
         * AWAKE
         */
        private void Awake()
        {
            //Debug.Log($"{GetType()} - Awake on Scene [ {SceneManager.GetActiveScene().name} ]");
            
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            LogErrorHelper.NotFoundWhatInIf(gameInputCursor == null, typeof(GameInputCursor).ToString(), this);

            _camera = Camera.main;
            _gameInputActions = new YusamPackageGameInputActions();
            _gameInputActions.DefaultMap.Enable();
            
            InputSystem.settings.SetInternalFeatureFlag("USE_OPTIMIZED_CONTROLS", true);
            InputSystem.settings.SetInternalFeatureFlag("USE_READ_VALUE_CACHING", true);
            InputSystem.settings.SetInternalFeatureFlag("PARANOID_READ_VALUE_CACHING_CHECKS", true);
            
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            
            InputState.Change(_virtualMouse.position, GetMousePosition());
            
            InputSystem.onAfterUpdate += InputSystemOnAfterUpdate;
        }

        /*
         * DESTROY
         */
        private void OnDestroy()
        {
            _gameInputActions.DefaultMap.Disable();
            
            InputSystem.onAfterUpdate -= InputSystemOnAfterUpdate;
            InputSystem.RemoveDevice(_virtualMouse);
            
            _gameInputActions.Dispose();
            
            //Debug.Log($"{GetType()} - OnDestroy on Scene [ {SceneManager.GetActiveScene().name} ]");
        }
        
        private void Update()
        {
            CursorAnchored(GetRightStickMousePosition()); 
        }

        private void InputSystemOnAfterUpdate()
        {
            var deltaValue = GetRightStickDirection(); 
            deltaValue *= virtualCursorSpeed * Time.deltaTime;

            var currentPosition = _virtualMouse.position.ReadValue();
            var newPosition = currentPosition + deltaValue;

            newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
            newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);
        
            InputState.Change(_virtualMouse.position, newPosition);
            InputState.Change(_virtualMouse.delta, deltaValue);
        }

        private void CursorAnchored(Vector2 newPosition)
        {
    
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    gameInputCursor.GetRectTransformCanvas(),
                    newPosition,
                    gameInputCursor.GetCanvas().renderMode == RenderMode.ScreenSpaceOverlay ? null : _camera,
                    out var anchoredPosition
                ))
            {
                gameInputCursor.GetRectTransformCursor().anchoredPosition = anchoredPosition;
                return;
            }
        
            gameInputCursor.GetRectTransformCursor().anchoredPosition = newPosition;   
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
                
                case GameInputPerformedEnum.RightStickMousePrimaryPress:
                    return GetRightStickMousePrimaryPressAction();  
                case GameInputPerformedEnum.RightStickMouseSecondaryPress:
                    return GetRightStickMouseSecondaryPressAction();     
            }
            LogErrorHelper.NotImplementedWhatIn(typeof(GameInputPerformedEnum) + $" : {gameInputPerformedEnum}", this);
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
        
        
        public InputAction GetRightStickMousePositionAction()
        {
            return _gameInputActions.DefaultMap.RightStickMousePosition;
        }
        
        public Vector2 GetRightStickMousePosition()
        {
            return GetRightStickMousePositionAction().ReadValue<Vector2>();
        }
        
        
        public InputAction GetRightStickMousePrimaryPressAction()
        {
            return _gameInputActions.DefaultMap.RightStickMousePrimaryPress;
        }
        
        public InputAction GetRightStickMouseSecondaryPressAction()
        {
            return _gameInputActions.DefaultMap.RightStickMouseSecondaryPress;
        }

        public Vector3 GetLeftStickDirectionAsVector3()
        {
            var leftStickDirection = GetLeftStickDirection();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        
        private void OnGUI()
        {
            if (!debugInDisplay) return;
            if (!DebugDisplay.HasInstance()) return;
            if (!DebugDisplay.Instance.DebugEnabled()) return;
            
            DebugDisplay.Instance.DisplayIndex(
                DebugDisplay.GAME_INPUT_GET_MOUSE_POSITION,
                $"Mouse Position: {GetMousePosition()}"
                );
            DebugDisplay.Instance.DisplayIndex(
                DebugDisplay.GAME_INPUT_GET_VIRTUAL_MOUSE_POSITION, 
                $"Virtual Mouse Position: {GetVirtualMousePosition()}"
                );
            DebugDisplay.Instance.DisplayIndex(
                DebugDisplay.GAME_INPUT_GET_RIGHT_STICK_MOUSE_POSITION, 
                $"Right Stick && Mouse Position: {GetRightStickMousePosition()}"
                );
        }
    }
}
