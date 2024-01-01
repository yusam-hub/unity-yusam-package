using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{

    public class GameInputScene : MonoBehaviour
    {
       
        [SerializeField] private GameInputSceneSo[] availableGameInputSceneArray;

        [YusamDropdownInt("AvailableSceneStringList()")]
        [SerializeField] private int activeSceneIndex;

        private List<String> _availableList = new List<string>();

        public void StoreEditorChanged()
        {
            _availableList.Clear();
            foreach (GameInputSceneSo gameInputSceneSo in availableGameInputSceneArray)
            {
                if (gameInputSceneSo != null)
                {
                    _availableList.Add(gameInputSceneSo.title);
                }
            }
        }
        
        public List<string> AvailableSceneStringList()
        {
            return _availableList;
        }
        
        private void OnValidate()
        {
            StoreEditorChanged();
        }
    }
}
