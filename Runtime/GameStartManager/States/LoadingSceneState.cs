using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YusamPackage
{
    public class LoadingSceneState : GameStartManagerState
    {
        [SerializeField] private GameObject loadingSceneUi;
        [SerializeField] private string sceneName;

        private float _loadingTimer;
        private bool _waitForSceneLoaded;
        
        public override void Enter()
        {
            loadingSceneUi.SetActive(true);

            GetGameStartManager().OnSceneLoaded += OnOnSceneLoaded;
            
            GetGameStartManager().DoStartLoadingScene(sceneName);
        }

        private void OnOnSceneLoaded(object sender, EventArgs e)
        {
            _waitForSceneLoaded = true;
        }

        public override void Exit()
        {
            GetGameStartManager().OnSceneLoaded -= OnOnSceneLoaded;
            
            loadingSceneUi.SetActive(false);
        }

        public override void Update()
        {
            if (_waitForSceneLoaded && !isFinished)
            {
                isFinished = true;
            }
        }
        
        public override GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum()
        {
            return GameStartManager.GameStartManagerStateEnum.LoadingScene;
        }

    }
}