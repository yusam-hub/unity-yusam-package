using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameState
{
    public class GameStateMachineDictionary
    {
        private readonly Dictionary<Type, IGameState> _gameStates = new() {
        };
        private IGameState _currentGameState;

        public void TryRegisterState(IGameState gameState)
        {
            _gameStates.TryAdd(gameState.GetType(), gameState);
        }

        public void Run<TGameState>() where TGameState : IGameState
        {
            if (_gameStates.TryGetValue(typeof(TGameState), out var state))
            {
                _currentGameState?.Exit();
                _currentGameState = state;
                _currentGameState?.Enter();
            }
        }
        
        public bool HasCurrentGameState()
        {
            return _currentGameState != null;
        }

        public IGameState GetCurrentGameState()
        {
            return _currentGameState;
        }
    }
}
