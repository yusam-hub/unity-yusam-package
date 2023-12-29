using System;
using UnityEngine;

namespace YusamPackage.GameState
{
    public class GameStateUpdateMachineSo: MonoBehaviour
    {
        [SerializeField] private GameStateSo startGameState;

        private GameStateSo _currentGameStateSo;
        
        private void Start()
        {
            SetGameStateSo(startGameState);
        }

        private void Update()
        {
            GameStateSoUpdate();
        }
        
        public virtual void SetGameStateSo(GameStateSo gameStateSo)
        {
            if (_currentGameStateSo)
            {
                _currentGameStateSo.Exit();
            }

            if (gameStateSo)
            {
                _currentGameStateSo = Instantiate(gameStateSo);
                _currentGameStateSo.gameStateUpdateMachineSo = this;
                _currentGameStateSo.Enter();
            }
            else
            {
                _currentGameStateSo = null;
            }
        }

        protected virtual void GameStateSoUpdate()
        {
            if (!_currentGameStateSo) return;
            
            if (!_currentGameStateSo.IsFinished)
            {
                _currentGameStateSo.Update();
            }
            else
            {
                //do other logic
            }
        }
    }
}
