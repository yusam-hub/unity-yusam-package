using System;
using UnityEngine;

namespace YusamPackage
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public enum GameManagerState
        {
            StaringGame,
            MainScreenLoader,
            MainMenu
        }
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
        

        //private GameManagerState _gameManagerState;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
            DontDestroyOnLoad(this);
            
            //_gameManagerState = GameManagerState.StaringGame;
            //SetGameStateSo(startGameState);
        }

        private void Update()
        {
            /*if (_currentGameStateSo)
            {
                _currentGameStateSo.Update();
            }
            else
            {
                switch (_gameManagerState)
                {
                    case GameManagerState.StaringGame:
                        _gameManagerState = GameManagerState.MainScreenLoader;
                        SetGameStateSo(mainScreenLoaderSo);
                        break;
                    case GameManagerState.MainScreenLoader:
                        _gameManagerState = GameManagerState.MainMenu;
                        SetGameStateSo(mainMenuSo);
                        break;                    
                }
            }*/
        }
    }
}