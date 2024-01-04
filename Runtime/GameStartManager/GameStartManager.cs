using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class GameStartManager : MonoBehaviour
    {
        public static GameStartManager Instance { get; private set; }
        public enum GameStartManagerStateEnum
        {
            StartingGame,
            MainScreenLoader,
            MainMenu,
            LoadingScene,
            LoadedScene
        }
        
        public AsyncOperation asyncOperation;
        
        public GameStartManagerStateEnum currentManagerStateEnum = GameStartManagerStateEnum.StartingGame;

        private Dictionary<GameStartManagerStateEnum, GameStartManagerState> _states = new Dictionary<GameStartManagerStateEnum, GameStartManagerState>();

        private GameStartManagerState _currentGameManagerState;
        
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
                        _currentGameManagerState.Enter();
                    }
                }
            }
        }
        private void Update()
        {
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