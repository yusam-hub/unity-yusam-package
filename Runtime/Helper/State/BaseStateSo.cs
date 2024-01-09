using UnityEngine;

namespace YusamPackage
{
    public abstract class BaseStateSo : ScriptableObject
    {
        private BaseStateMachine _baseStateMachine;

        public virtual void SetStateMachine(BaseStateMachine stateMachine)
        {
            _baseStateMachine = stateMachine;
        }
        public BaseStateMachine GetStateMachine()
        {
            return _baseStateMachine;
        }
        public virtual void EnterState(){}
        public abstract void UpdateState();
        public virtual void ExitState(){}
    }
}