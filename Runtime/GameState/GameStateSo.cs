using UnityEngine;

namespace YusamPackage
{
    public abstract class GameStateSo : ScriptableObject
    {
        [HideInInspector] 
        public GameStateMachineSo gameStateMachineSo;
        
        public virtual void Enter()
        {
            GameDebug.Log("Enter: " + this);  
        }

        public virtual void Exit()
        {
            GameDebug.Log("Exit: " + this); 
        }
        public abstract void Update();

    }
}
