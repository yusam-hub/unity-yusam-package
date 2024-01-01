using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace YusamPackage
{
    public class LoadingSceneState : GameManagerState
    {
        [SerializeField] private GameInput gameInput;
        [SerializeField] private GameInput.GameInputPerformedEnum[] pressKeyArray;
        [SerializeField] private GameObject loadingSceneUi;
        [SerializeField] private string sceneName;

        private float _loadingTimer;
        private AsyncOperation _asyncOperation;
        private bool _loadingSceneFinished;
        private bool _isFinished;
        
        public override void Enter()
        {
            loadingSceneUi.SetActive(true);
            StartCoroutine("AsyncSceneLoading", sceneName);
            foreach (GameInput.GameInputPerformedEnum gameInputPerformedEnum in pressKeyArray)
            {
                if (gameInputPerformedEnum == GameInput.GameInputPerformedEnum.None) continue;
                gameInput.GetActionByEnum(gameInputPerformedEnum).performed += OnPerformed;
            }
        }

        private void OnPerformed(InputAction.CallbackContext obj)
        {
            _loadingSceneFinished = true;
        }

        IEnumerator AsyncSceneLoading(string aSceneName)
        {
            float loadingProgress;
            _asyncOperation = SceneManager.LoadSceneAsync(aSceneName);
            _asyncOperation.allowSceneActivation = false;
            while (_asyncOperation.progress < 0.9f)
            {
                _loadingTimer += Time.deltaTime;
                loadingProgress = Mathf.Clamp01(_asyncOperation.progress / 0.9f);
                Debug.Log($"Loading: {(loadingProgress * 100).ToString("0")}%");
                yield return true;
            }

            Debug.Log("AsyncSceneLoading finished: " + _loadingTimer);
            
            while (_loadingTimer < 2f)
            {
                _loadingTimer += Time.deltaTime;
                yield return true;
            }
            
            Debug.Log("Total finished: " + _loadingTimer);

           
        }
        
        public override void Exit()
        {
            foreach (GameInput.GameInputPerformedEnum gameInputPerformedEnum in pressKeyArray)
            {
                if (gameInputPerformedEnum == GameInput.GameInputPerformedEnum.None) continue;
                gameInput.GetActionByEnum(gameInputPerformedEnum).performed -= OnPerformed;
            }
            loadingSceneUi.SetActive(false);
        }

        public override void Update()
        {
            if (_loadingSceneFinished && !_isFinished)
            {
                _isFinished = true;
                Exit();
                _asyncOperation.allowSceneActivation = true;
            }
        }
        
        public override GameManager.GameManagerStateEnum GetGameManagerStateEnum()
        {
            return GameManager.GameManagerStateEnum.LoadingScene;
        }
        
        
    }
}