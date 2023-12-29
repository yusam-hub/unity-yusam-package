using System;
using UnityEngine;

namespace YusamPackage.GameState
{
    public class GameStateUpdateMachine: MonoBehaviour
    {
        [SerializeField] private GameStateSo startGameState;

        [HideInInspector]
        public GameStateSo currentGameStateSo;
        
        private void Start()
        {
            SetGameStateSo(startGameState);
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

        private void Update()
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
