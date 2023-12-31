using UnityEngine;

namespace YusamPackage
{
    public abstract class GameStateSo : ScriptableObject
    {
        [HideInInspector] 
        public GameStateMachineSo gameStateMachineSo;
        
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
