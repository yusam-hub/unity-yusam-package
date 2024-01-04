using UnityEngine;

namespace YusamPackage
{
    public abstract class GameStartManagerState : MonoBehaviour
    {
        public abstract GameStartManager.GameStartManagerStateEnum GetGameStartManagerStateEnum();

        protected bool isFinished;
        
        private GameStartManager _gameStartManager;

        public void SetGameStartManager(GameStartManager gameStartManager)
        {
            _gameStartManager = gameStartManager;
        }

        public GameStartManager GetGameStartManager()
        {
            return _gameStartManager;
        }
        

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public bool IsFinished()
        {
            return isFinished;
        }
    }
}