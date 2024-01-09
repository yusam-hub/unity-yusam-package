using System;
using UnityEngine;

namespace YusamPackage
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseStateSo startStateSo;

        private BaseStateSo _currentStateSo;

        private void Awake()
        {
            InitStateMachine();
        }

        protected virtual void InitStateMachine()
        {
            ChangeCurrentStateSo(startStateSo); 
        }

        public BaseStateSo GetCurrentStateSo()
        {
            return _currentStateSo;
        }
        
        public virtual void ChangeCurrentStateSo(BaseStateSo newStateSo)
        {
            if (_currentStateSo)
            {
                _currentStateSo.ExitState();
            }
            
            _currentStateSo = null;
            
            if (newStateSo)
            {
                _currentStateSo = Instantiate(newStateSo);
                _currentStateSo.SetStateMachine(this);
                _currentStateSo.EnterState();
            }
        }

        protected virtual void UpdateStateMachine()
        {
            if (_currentStateSo)
            {
                _currentStateSo.UpdateState(); 
            }
        }
        
        private void Update()
        {
            UpdateStateMachine();
        }
    }
}