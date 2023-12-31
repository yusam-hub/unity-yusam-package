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
            GameDebug.Log("Enter: " + this);  
        }

        public abstract void Update();
        
        public virtual void Exit()
        {
            GameDebug.Log("Exit: " + this); 
        }

        public GameStateMachineDictionary GetGameStateMachine()
        {
            return _gameStateMachineDictionary;
        }
    }
}
