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
        private bool _loadingSceneFinished;
        
        public override void Enter()
        {
            loadingSceneUi.SetActive(true);
            StartCoroutine(AsyncSceneLoading(sceneName));
        }

        IEnumerator AsyncSceneLoading(string aSceneName)
        {
            float loadingProgress;
            GetGameStartManager().asyncOperation = SceneManager.LoadSceneAsync(aSceneName);
            GetGameStartManager().asyncOperation.allowSceneActivation = false;
            while (GetGameStartManager().asyncOperation.progress < 0.9f)
            {
                _loadingTimer += Time.deltaTime;
                loadingProgress = Mathf.Clamp01(GetGameStartManager().asyncOperation.progress / 0.9f);
                Debug.Log($"Loading: {(loadingProgress * 100).ToString("0")}%");
                yield return true;
            }

            while (_loadingTimer < 2f)
            {
                _loadingTimer += Time.deltaTime;
                yield return true;
            }
            
            _loadingSceneFinished = true;
        }
        
        public override void Exit()
        {
            loadingSceneUi.SetActive(false);
        }

        public override void Update()
        {
            if (_loadingSceneFinished && !isFinished)
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