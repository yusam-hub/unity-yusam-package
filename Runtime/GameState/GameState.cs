using UnityEngine;

namespace YusamPackage
{
    public abstract class GameState : IGameState
    {
        private readonly GameStateMachineDictionary _gameStateMachineDictionary;

        protected GameState(GameStateMachineDictionary gameStateMachineDictionary)
        {
            _gameStateMachineDictionary = gameStateMachineDictionary;
        }

        public virtual void Enter()
        {
            Debug.Log($"{GetType()} - Enter");
        }

        public virtual void Update()
        {
            
        }
        
        public virtual void Exit()
        {
            Debug.Log($"{GetType()} - Edit");
        }

        public GameStateMachineDictionary GetGameStateMachine()
        {
            return _gameStateMachineDictionary;
        }
    }
}
