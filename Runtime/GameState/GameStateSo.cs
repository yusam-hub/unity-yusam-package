using UnityEngine;

namespace YusamPackage
{
    public abstract class GameStateSo : ScriptableObject
    {
        [HideInInspector] 
        public GameStateMachineSo gameStateMachineSo;
        
        public void Enter()
        {
            Debug.Log($"{GetType()} - Enter");
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} - Edit");
        }

        public virtual void Update()
        {
            
        }

    }
}
