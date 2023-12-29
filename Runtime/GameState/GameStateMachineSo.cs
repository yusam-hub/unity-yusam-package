using System;
using UnityEngine;

namespace YusamPackage.GameState
{
    public class GameStateUpdateMachineSo: MonoBehaviour
    {
        [SerializeField] private GameStateSo startGameState;

        [HideInInspector]
        public GameStateSo currentGameStateSo;
        
        private void Start()
        {
            SetGameStateSo(startGameState);
        }

        private void Update()
        {
            GameStateSoUpdate();
        }
        
        protected virtual void SetGameStateSo(GameStateSo gameStateSo)
        {
            if (currentGameStateSo)
            {
                currentGameStateSo.Exit();
            }

            if (gameStateSo)
            {
                currentGameStateSo = Instantiate(gameStateSo);
                currentGameStateSo.parentGameObject = gameObject;
                currentGameStateSo.Enter();
            }
            else
            {
                currentGameStateSo = null;
            }
        }

        protected virtual void GameStateSoUpdate()
        {
            if (!currentGameStateSo) return;
            
            if (!currentGameStateSo.IsFinished)
            {
                currentGameStateSo.Update();
            }             
            else
            {
                SetGameStateSo(currentGameStateSo.nextGameState);
            } 
        }
    }
}
