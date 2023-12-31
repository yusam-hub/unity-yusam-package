using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YusamPackage
{
    public class LoadingSceneState : GameManagerState
    {
        [SerializeField] private GameObject loadingSceneUi;
        [SerializeField] private string sceneName;

        private bool _isFinished;
        private float _loadingTimer;
        private AsyncOperation _asyncOperation;
        private bool _loadingSceneFinished;
        
        public override void Enter()
        {
            loadingSceneUi.SetActive(true);
            StartCoroutine("AsyncSceneLoading", sceneName);
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

            _loadingSceneFinished = true;
        }
        
        public override void Exit()
        {
            loadingSceneUi.SetActive(false);
        }

        public override void Update()
        {
            if (_loadingSceneFinished)
            {
                _asyncOperation.allowSceneActivation = true;
            }
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }
        
        public override GameManager.GameManagerStateEnum GetGameManagerStateEnum()
        {
            return GameManager.GameManagerStateEnum.LoadingScene;
        }
        
        
    }
}