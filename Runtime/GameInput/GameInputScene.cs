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
        [YusamHelpBox("Add to available list Scriptable Objects","",2, "#FF8000")]
        [SerializeField] private GameInputSceneSo[] availableGameInputSceneArray;
        
        [Space]
        [YusamHelpBox("Active scene can change any time","",2, "#FF8000")]
        [YusamDropdownInt("AvailableSceneStringList()")]
        [SerializeField] private int activeSceneIndex;

        [Space]
        [YusamHelpBox("Active layer can change only in runtime","",2, "#FF8000")]
        [YusamDropdownInt("AvailableLayerStringList()")]
        [SerializeField] private int activeLayerIndex;

        private List<String> _availableList = new List<string>();
        private Dictionary<string, IGameInputScene> _gameInputSceneDictionary;
        private IGameInputScene _activeGameInputScene;
        private IGameInputScene _lastGameInputScene;
        
        public void StoreSceneEditorChanged()
        {
            _availableList.Clear();

            if (availableGameInputSceneArray != null)
            {
                foreach (GameInputSceneSo gameInputSceneSo in availableGameInputSceneArray)
                {
                    if (gameInputSceneSo != null)
                    {
                        _availableList.Add(gameInputSceneSo.title);
                    }
                }
            }

            if (_activeGameInputScene != null)
            {
                _activeGameInputScene.StoreLayerEditorChanged();
            }
        }
        
        public List<string> AvailableSceneStringList()
        {
            return _availableList;
        }
        
        public List<string> AvailableLayerStringList()
        {
            if (_activeGameInputScene != null)
            {
                return _activeGameInputScene.AvailableLayerStringList();
            }
            return new List<string>();
        }
        
        private void OnValidate()
        {
            Debug.Log("OnValidate");
            StoreSceneEditorChanged();
            
            if (Application.isPlaying)
            {
                if (
                    _gameInputSceneDictionary != null 
                    && 
                    availableGameInputSceneArray != null 
                    && 
                    activeSceneIndex >= 0 
                    && 
                    activeSceneIndex < availableGameInputSceneArray.Length
                    )
                {
                    if (_gameInputSceneDictionary.TryGetValue(
                            availableGameInputSceneArray[activeSceneIndex].key,
                            out IGameInputScene gameInputScene))
                    {
                        _lastGameInputScene = gameInputScene;
                    }
                }
            }
        }

        private void Awake()
        {
            _gameInputSceneDictionary = new Dictionary<string, IGameInputScene>();

            int index = 0;
            foreach (GameInputSceneSo gameInputSceneSo in availableGameInputSceneArray)
            {
                if (gameInputSceneSo != null)
                {
                    IGameInputScene temp = Instantiate(gameInputSceneSo);
                    _gameInputSceneDictionary.Add(gameInputSceneSo.key, temp);
                    
                    if (activeSceneIndex == index)
                    {
                        _lastGameInputScene = temp;
                    }

                    index++;
                }
            }

            if (_lastGameInputScene == null && _gameInputSceneDictionary.Count > 0)
            {
                if (_gameInputSceneDictionary.TryGetValue(availableGameInputSceneArray[0].key, out IGameInputScene gameInputScene))
                {
                    _lastGameInputScene = gameInputScene;
                }
            }
        }

        private void Update()
        {
            if (_activeGameInputScene != _lastGameInputScene)
            {
                if (_activeGameInputScene != null)
                {
                    _activeGameInputScene.DoExit();
                }
                _activeGameInputScene = _lastGameInputScene;
                if (_activeGameInputScene != null)
                {
                    _activeGameInputScene.DoEnter();
                }
            } else if (_activeGameInputScene != null)
            {
                _activeGameInputScene.DoUpdate();
            }
        }

        private void OnDestroy()
        {
            if (_activeGameInputScene != null)
            {
                _activeGameInputScene.DoExit();
            }
        }
    }
}
