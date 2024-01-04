using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YusamPackage
{
    public class GameStartManager : MonoBehaviour
    {
        private static GameStartManager Instance { get; set; }
        public enum GameStartManagerStateEnum
        {
            StartingGame,
            MainScreenLoader,
            MainMenu,
            LoadingScene,
            LoadedScene
        }

        public event EventHandler OnSceneLoaded;
        
        public GameStartManagerStateEnum currentManagerStateEnum = GameStartManagerStateEnum.StartingGame;
        
        private readonly Dictionary<GameStartManagerStateEnum, GameStartManagerState> _states = new();
        private GameStartManagerState _currentGameManagerState;

        private float _loadingTimer;
        private float _loadingProgress;
        private AsyncOperation _asyncOperation;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;

            GameStartManagerState[] states = GetComponentsInChildren<GameStartManagerState>();

            foreach (GameStartManagerState state in states)
            {
                _states.TryAdd(state.GetGameStartManagerStateEnum(), state);
            }
        }
        
        public void DoStartLoadingScene(string sceneName)
        {
            if (_asyncOperation == null)
            {
                StartCoroutine(AsyncStartLoadingScene(sceneName));
            }
        }

        public void DoSceneLoadedConfirm()
        {
            if (_asyncOperation != null)
            {
                _asyncOperation.allowSceneActivation = true;
            }
        }
        
        IEnumerator AsyncStartLoadingScene(string aSceneName)
        {

 
            _asyncOperation = SceneManager.LoadSceneAsync(aSceneName);
            _asyncOperation.allowSceneActivation = false;
            while (_asyncOperation.progress < 0.9f)
            {
                _loadingTimer += Time.deltaTime;
                _loadingProgress = Mathf.Clamp01(_asyncOperation.progress / 0.9f);
                Debug.Log($"{GetType()} - Loading {(_loadingProgress * 100).ToString("0")}%");
                yield return true;
            }

            while (_loadingTimer < 2f)
            {
                _loadingTimer += Time.deltaTime;
                yield return true;
            }
            
            OnSceneLoaded?.Invoke(this, EventArgs.Empty);
        }
        


        private void TryGetCurrentManagerState()
        {
            if (_currentGameManagerState == null)
            {
                if (_states.TryGetValue(currentManagerStateEnum, out GameStartManagerState gameManagerState))
                {
                    _currentGameManagerState = gameManagerState;
                    _currentGameManagerState.SetGameStartManager(this);
                    _currentGameManagerState.Enter();
                }
            }
            else
            {
                if (_currentGameManagerState.GetGameStartManagerStateEnum() != currentManagerStateEnum)
                {
                    if (_states.TryGetValue(currentManagerStateEnum, out GameStartManagerState gameManagerState))
                    {
                        _currentGameManagerState.Exit();
                        
                        _currentGameManagerState = gameManagerState;
                        _currentGameManagerState.SetGameStartManager(this);
                        _currentGameManagerState.Enter();
                    }
                }
            }
        }
        private void Update()
        {
            if (_asyncOperation != null && _asyncOperation.allowSceneActivation)
            {
                return;
            }
            
            TryGetCurrentManagerState();

            if (_currentGameManagerState == null) return;

            if (!_currentGameManagerState.IsFinished())
            {
                _currentGameManagerState.Update();
            }
            else
            {
                switch (currentManagerStateEnum)
                {
                    case GameStartManagerStateEnum.StartingGame:
                        currentManagerStateEnum = GameStartManagerStateEnum.MainScreenLoader;
                        break;
                    case GameStartManagerStateEnum.MainScreenLoader:
                        currentManagerStateEnum = GameStartManagerStateEnum.MainMenu;
                        break;
                    case GameStartManagerStateEnum.MainMenu:
                        currentManagerStateEnum = GameStartManagerStateEnum.LoadingScene;
                        break;       
                    case GameStartManagerStateEnum.LoadingScene:
                        currentManagerStateEnum = GameStartManagerStateEnum.LoadedScene;
                        break;                      
                }    
            }
        }
    }
}