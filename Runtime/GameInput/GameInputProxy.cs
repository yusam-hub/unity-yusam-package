using System;
using System.Collections.Generic;
using UnityEngine;
using YusamPackage.DropdownAttributes;

namespace YusamPackage.GameInput
{
    public class GameInputProxy : MonoBehaviour
    {
        [SerializeField] private GameInputManager gameInputManager;

        public List<GameInput> gameInputList;
    
        public List<GameInput> GetGameInputList(){
            return gameInputList;
        }
        [Dropdown("GetGameInputList()", "name")]
        public GameInput selectedGameInput;

        private void Awake()
        {
            if (selectedGameInput == null)
            {
                Debug.LogError("Selected Game Input is null");
            }
        }

        public GameInputManager GetGameInputManager()
        {
            return gameInputManager;
        }

        public bool GetGameInputEnabled(GameInput gameInput)
        {
            return selectedGameInput == gameInput;
        }
    }
}