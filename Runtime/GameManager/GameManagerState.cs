using UnityEngine;

namespace YusamPackage
{
    public abstract class GameManagerState : MonoBehaviour, IGameManagerState
    {
        public abstract GameManager.GameManagerStateEnum GetGameManagerStateEnum();

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }
        public abstract void Update();

        public virtual bool IsFinished()
        {
            return false;
        }
    }
}