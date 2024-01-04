using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace YusamPackage
{
    public class LoadedSceneState : GameStartManagerState
    {
        [SerializeField] private GameInput gameInput;
        [SerializeField] private GameInputPerformedEnum[] pressKeyArray;
        [SerializeField] private GameObject loadedSceneUi;

        private bool _loadingSceneFinished;
        
        public override void Enter()
        {
            loadedSceneUi.SetActive(true);
            foreach (GameInputPerformedEnum gameInputPerformedEnum in pressKeyArray)
            {
                if (gameInputPerformedEnum == GameInputPerformedEnum.None) continue;
                gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnPerformed;
            }
        }

        private void OnPerformed(InputAction.CallbackContext obj)
        {
            _loadingSceneFinished = true;
        }
       
        public override void Exit()
        {
            foreach (GameInputPerformedEnum gameInputPerformedEnum in pressKeyArray)
            {
                if (gameInputPerformedEnum == GameInputPerformedEnum.None) continue;
                gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnPerformed;
            }
            loadedSceneUi.SetActive(false);
        }

        public override void Update()
        {
            if (_loadingSceneFinished && !isFinished)
            {
                Exit();
                GetGameStartManager().asyncOperation.allowSceneActivation = true;
            }
        }
        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.LoadedScene;
        }

    }
}