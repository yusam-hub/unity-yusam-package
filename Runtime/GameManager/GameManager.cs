using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public enum GameManagerStateEnum
        {
            StartingGame,
            MainScreenLoader,
            MainMenu
        }
        
        [SerializeField] private GameManagerStateEnum currentManagerStateEnum = GameManagerStateEnum.StartingGame;

        /*
         * Минимальные состояния игры (все состояния могут в любой момент меняться и дополняться)
         * 1) главный экран загрузки игры
         * 2) главное меню (опции, новая, загрузка, выход и т.д)
         * 3) выбор сцены (опционно)
         * 4) заставка асинхронной загрузки сцены и ресурсов
         * 5) заставка начала игры (все загружено и что бы начать нажмите клавишу)
         * 6) игра
         *     6.1 игра активна
         *     6.2 пауза игры (экран паузы, возврат в игру (продолжить), настройки, выйти в главное меню
         * 8) экран окончания игры
         *     8.1. успешный
         *     9.1 не успешный (возврат в главное меню или перегиграть уровень заново
         * 9) загрузка следующего уровня (или сначала)
         */

        private Dictionary<GameManagerStateEnum, IGameManagerState> _states =
            new Dictionary<GameManagerStateEnum, IGameManagerState>();

        private IGameManagerState _currentGameManagerState;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
            DontDestroyOnLoad(this);

            IGameManagerState[] states = GetComponentsInChildren<IGameManagerState>();

            foreach (IGameManagerState state in states)
            {
                _states.TryAdd(state.GetGameManagerStateEnum(), state);
            }
        }

        private void TryGetCurrentManagerState()
        {
            if (_currentGameManagerState == null)
            {
                if (_states.TryGetValue(currentManagerStateEnum, out IGameManagerState gameManagerState))
                {
                    _currentGameManagerState = gameManagerState;
                    _currentGameManagerState.Enter();
                }
            }
            else
            {
                if (_currentGameManagerState.GetGameManagerStateEnum() != currentManagerStateEnum)
                {
                    if (_states.TryGetValue(currentManagerStateEnum, out IGameManagerState gameManagerState))
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
                    case GameManagerStateEnum.StartingGame:
                        currentManagerStateEnum = GameManagerStateEnum.MainScreenLoader;
                        break;
                    case GameManagerStateEnum.MainScreenLoader:
                        currentManagerStateEnum = GameManagerStateEnum.MainMenu;
                        break;
                }    
            }
        }
    }
}