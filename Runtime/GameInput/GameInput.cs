using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInput : MonoBehaviour
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("GameInput - контроллер управления, быть статичным статичным и один на сцене")]
#endif        
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Project Settings -> Player -> Active Input Handling = Both | Input System Package (New)")]
#endif        
        [Space(10)]
        [SerializeField] private GameInputCursor gameInputCursor;
        [Space(10)]
        [SerializeField] private float virtualCursorSpeed = 1000;

        public static GameInput Instance { get; private set; }

        private YusamPackageGameInputActions _gameInputActions;
        private Mouse _virtualMouse;
        private YusamDebugProperties _yusamDebugProperties;
        public static bool HasInstance()
        {
            return Instance;
        }
        
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

            LogErrorHelper.NotFoundWhatInIf(gameInputCursor == null, typeof(GameInputCursor).ToString(), this);
            
            _yusamDebugProperties = GetComponent<YusamDebugProperties>();
            
            _gameInputActions = new YusamPackageGameInputActions();
            _gameInputActions.DefaultMap.Enable();
            
            Debug.Log($"{GetType()} - Awake AddDevice 1 ");
            /*
             * todo: при загрузке новой сцены сюда ругается
             *      Could not find active control after binding resolution
             *      UnityEngine.InputSystem.InputSystem:AddDevice (string,string,string)
             */
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            Debug.Log($" {GetType()} - Awake AddDevice 2");
            
            InputState.Change(_virtualMouse.position, GetMousePosition());
            
            InputSystem.onAfterUpdate += InputSystemOnAfterUpdate;
        }

        /*
         * DESTROY
         */
        private void OnDestroy()
        {
            InputSystem.onAfterUpdate -= InputSystemOnAfterUpdate;
            InputSystem.RemoveDevice(_virtualMouse);
            
            _gameInputActions.Dispose();
            
            Debug.Log($"{GetType()} - OnDestroy");
        }
        
        private void Update()
        {
            CursorAnchored(GetRightStickMousePosition()); 
        }

        private void InputSystemOnAfterUpdate()
        {
            Vector2 deltaValue = GetRightStickDirection(); 
            deltaValue *= virtualCursorSpeed * Time.deltaTime;

            Vector2 currentPosition = _virtualMouse.position.ReadValue();
            Vector2 newPosition = currentPosition + deltaValue;

            newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
            newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);
        
            InputState.Change(_virtualMouse.position, newPosition);
            InputState.Change(_virtualMouse.delta, deltaValue);
        }

        private void CursorAnchored(Vector2 newPosition)
        {
            Vector2 anchoredPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    gameInputCursor.GetRectTransformCanvas(),
                    newPosition,
                    gameInputCursor.GetCanvas().renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
                    out anchoredPosition
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
            LogErrorHelper.NotImplementedWhatIn(typeof(GameInputPerformedEnum).ToString() + $" : {gameInputPerformedEnum}", this);
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
            Vector2 leftStickDirection = GetLeftStickDirection();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        
        private void OnGUI()
        {
            if (!YusamDebugDisplay.HasInstance()) return;
            if (!YusamDebugDisplay.Instance.DebugEnabled()) return;
            
            YusamDebugDisplay.Instance.DisplayLog(
                YusamDebugDisplay.GAME_INPUT_GET_MOUSE_POSITION,
                $"Mouse Position: {GetMousePosition()}"
                );
            YusamDebugDisplay.Instance.DisplayLog(
                YusamDebugDisplay.GAME_INPUT_GET_VIRTUAL_MOUSE_POSITION, 
                $"Virtual Mouse Position: {GetVirtualMousePosition()}"
                );
            YusamDebugDisplay.Instance.DisplayLog(
                YusamDebugDisplay.GAME_INPUT_GET_RIGHT_STICK_MOUSE_POSITION, 
                $"Right Stick && Mouse Position: {GetRightStickMousePosition()}"
                );
        }
    }
}
