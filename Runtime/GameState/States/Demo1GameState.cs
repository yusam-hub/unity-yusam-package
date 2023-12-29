using UnityEngine;

namespace YusamPackage.GameState
{
    public class Demo1GameState : GameState
    {
        public Demo1GameState(GameStateMachineDictionary gameStateMachineDictionary) : base(gameStateMachineDictionary)
        {
 
        }

        public override void Enter()
        {
            base.Enter();
            GetGameStateMachine().Run<Demo2GameState>();
        }
        
        public override void Update()
        {
          
        }
    }
}