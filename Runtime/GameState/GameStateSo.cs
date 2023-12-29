using UnityEngine;

namespace YusamPackage.GameState
{
    public abstract class GameStateSo : ScriptableObject
    {
        [HideInInspector] 
        public GameStateUpdateMachineSo gameStateUpdateMachineSo;
        
        public bool IsFinished { get; protected set; }
        
        public virtual void Enter()
        {
            Debug.Log("Enter: " + this);  
        }

        public virtual void Exit()
        {
            Debug.Log("Exit: " + this); 
        }
        public abstract void Update();

    }
}
