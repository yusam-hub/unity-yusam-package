using UnityEngine;
using UnityEngine.InputSystem;

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
            foreach (var gameInputPerformedEnum in pressKeyArray)
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
            foreach (var gameInputPerformedEnum in pressKeyArray)
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
                GetGameStartManager().DoSceneLoadedConfirm();
            }
        }
        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.LoadedScene;
        }

    }
}