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
        [SerializeField] private int activeGameInputSceneIndex;

        [YusamDropdownInt("AvailableLayerStringList()")]
        [SerializeField] private int activeGameInputLayerIndex;

        private List<String> _availableList = new List<string>();
        private Dictionary<string, IGameInputScene> _gameInputSceneDictionary;
        private IGameInputScene _activeGameInputScene;
        private IGameInputScene _lastGameInputScene;
        
        public void StoreSceneEditorChanged()
        {
            _availableList.Clear();
            foreach (GameInputSceneSo gameInputSceneSo in availableGameInputSceneArray)
            {
                if (gameInputSceneSo != null)
                {
                    _availableList.Add(gameInputSceneSo.title);
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
            StoreSceneEditorChanged();
            if (Application.isPlaying && (_gameInputSceneDictionary != null) && (availableGameInputSceneArray != null))
            {
                if (activeGameInputSceneIndex >= 0 && activeGameInputSceneIndex < availableGameInputSceneArray.Length)
                {
                    if (_gameInputSceneDictionary.TryGetValue(
                            availableGameInputSceneArray[activeGameInputSceneIndex].key,
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
                    
                    if (activeGameInputSceneIndex == index)
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
                if (_lastGameInputScene != null)
                {
                    _lastGameInputScene.DoExit();
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
