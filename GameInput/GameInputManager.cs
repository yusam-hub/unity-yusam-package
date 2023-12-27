using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YusamPackage.GameDebug;

namespace YusamPackage.GameInput
{
    public class GameInputManager : MonoBehaviour, IGameInputManager
    {
        public static GameInputManager Instance { get; private set; }

        
        [SerializeField] private bool isDebugging = false;
        [SerializeField] private GameDebugGridUi gameDebugGridUi; 
        
        private GameInputActions _gameInputActions;
        
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
            _gameInputActions.DefaultMap.AInteractAction.performed += AInteractActionOnPerformed;
            _gameInputActions.DefaultMap.BInteractAction.performed += BInteractActionOnPerformed;
            
        }

        private void Start()
        {

        }

        private void OnDestroy()
        {
            _gameInputActions.DefaultMap.AInteractAction.performed -= AInteractActionOnPerformed;
            _gameInputActions.DefaultMap.BInteractAction.performed -= BInteractActionOnPerformed;
            _gameInputActions.Dispose();
        }

        private void AInteractActionOnPerformed(InputAction.CallbackContext obj)
        {
            //Debug.Log("AInteractAction"); 
        }
        
        private void BInteractActionOnPerformed(InputAction.CallbackContext obj)
        {
            //Debug.Log("BInteractAction");   
        }

        public Vector2 GetMoveAsVector2Normalized()
        {
            return _gameInputActions.DefaultMap.MoveAction.ReadValue<Vector2>();
        }

        public Vector3 GetMoveAsHorizontalVector3Normalized()
        {
            Vector2 move = GetMoveAsVector2Normalized();
            return new Vector3(move.x, 0, move.y);
        }

        private void Update()
        {
            if (isDebugging)
            {
                if (gameDebugGridUi && gameDebugGridUi.GetCountCells() > 0)
                {
                    gameDebugGridUi.SetCellText(0, "Move: " + GetMoveAsVector2Normalized());            
                }
                gameDebugGridUi.SetCellText(1, "AInteractAction: " + (_gameInputActions.DefaultMap.AInteractAction.IsPressed() ? "IsPressed" : ""));     
                gameDebugGridUi.SetCellText(2, "BInteractAction: " + (_gameInputActions.DefaultMap.BInteractAction.IsPressed() ? "IsPressed" : ""));     
            }
        }
    }
}
