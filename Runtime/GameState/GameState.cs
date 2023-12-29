using UnityEngine;

namespace YusamPackage.GameState
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
            Debug.Log("Enter: " + this);  
        }

        public abstract void Update();
        
        public virtual void Exit()
        {
            Debug.Log("Exit: " + this); 
        }

        public GameStateMachineDictionary GetGameStateMachine()
        {
            return _gameStateMachineDictionary;
        }
    }
}
