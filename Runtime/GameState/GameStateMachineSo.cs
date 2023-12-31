using System;
using UnityEngine;

namespace YusamPackage
{
    public class GameStateMachineSo: MonoBehaviour
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
        
        public bool HasCurrentGameStateSo()
        {
            return _currentGameStateSo != null;
        }
        
        public GameStateSo GetCurrentGameStateSo()
        {
            return _currentGameStateSo;
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
                _currentGameStateSo.gameStateMachineSo = this;
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
            
            _currentGameStateSo.Update();
        }
    }
}
