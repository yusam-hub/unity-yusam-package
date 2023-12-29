using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameState
{
    public class GameStateMachine : MonoBehaviour
    {
        private GameStateMachineDictionary _gameStateMachineDictionary;

        private void Awake()
        {
            _gameStateMachineDictionary = new GameStateMachineDictionary();
            _gameStateMachineDictionary.TryRegisterState(new Demo1GameState(_gameStateMachineDictionary));
            _gameStateMachineDictionary.TryRegisterState(new Demo2GameState(_gameStateMachineDictionary));
        }

        private void Start()
        {
            _gameStateMachineDictionary.Run<Demo1GameState>();
        }

        private void Update()
        {
            if (_gameStateMachineDictionary.HasCurrentGameState())
            {
                _gameStateMachineDictionary.GetCurrentGameState().Update();
            }
        }
    }
}
