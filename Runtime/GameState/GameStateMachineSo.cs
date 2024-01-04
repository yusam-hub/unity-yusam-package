using UnityEngine;

namespace YusamPackage
{
    public class GameStateMachineSo: MonoBehaviour
    {
        [SerializeField] protected GameStateSo startGameState;

        private GameStateSo _currentGameStateSo;
        
        private void Awake()
        {
            SetGameStateSo(startGameState);
        }

        private void Update()
        {
            if (!_currentGameStateSo) return;
            
            _currentGameStateSo.Update();
        }
        
        public void SetGameStateSo(GameStateSo gameStateSo)
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
    }
}
