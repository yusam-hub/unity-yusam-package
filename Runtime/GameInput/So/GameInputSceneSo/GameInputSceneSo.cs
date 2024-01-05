using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Scene")]
    public class GameInputSceneSo : ScriptableObject
    {
        public GameInputLayerSo[] availableLayerSoArray;
        public string defaultLayerKey;
        public string key;
        public string title;

        private readonly List<String> _availableList = new();
        private Dictionary<string, GameInputLayerSo> _gameInputLayerDictionary;
        private GameInputLayerSo _activeGameInputLayer;
        private GameInputLayerSo _lastGameInputLayer;

        private GameInputScene _gameInputScene;

        public void SetGameInputScene(GameInputScene gameInputScene)
        {
            _gameInputScene = gameInputScene;
        }

        public void Reset()
        {
            _lastGameInputLayer = null;
        }
        
        public void StoreLayerEditorChanged()
        {
            _availableList.Clear();
            foreach (var gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo)
                {
                    _availableList.Add(gameInputLayerSo.title);
                }
            }
        }
        
        public List<string> AvailableLayerStringList()
        {
            return _availableList;
        }

        public void DoEditorChangeLayerIndex(int index)
        {
            //Debug.Log($"DoEditorChangeLayerIndex on {name} {key} {title}");
            if (_gameInputLayerDictionary != null && _gameInputLayerDictionary.Count > 0)
            {
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[index].key, out var gameInputLayer))
                {
                    _lastGameInputLayer = gameInputLayer;
                }
            }
        }
        
        public void DoEnter()
        {
            //Debug.Log($"Enter on {name} {key} {title}");
            
            StoreLayerEditorChanged();
            
            if (_gameInputLayerDictionary == null)
            {
                _gameInputLayerDictionary = new Dictionary<string, GameInputLayerSo>();
            }
            else
            {
                foreach (var p in _gameInputLayerDictionary)
                {
                    Destroy(p.Value);
                }

                _gameInputLayerDictionary.Clear();
            }


            var index = 0;
            foreach (var gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo)
                {
                    var temp = Instantiate(gameInputLayerSo);
                    //temp - can doit somethis
                    _gameInputLayerDictionary.Add(gameInputLayerSo.key, temp);
                    
                    if (defaultLayerKey == gameInputLayerSo.key)
                    {
                        _lastGameInputLayer = temp;
                    }

                    index++;
                }
            }

            if (!_lastGameInputLayer && _gameInputLayerDictionary.Count > 0)
            {
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[0].key, out var gameInputLayer))
                {
                    _lastGameInputLayer = gameInputLayer;
                }
            }
            
        }

        public void DoUpdate()
        {
            if (_activeGameInputLayer != _lastGameInputLayer)
            {
                if (_activeGameInputLayer)
                {
                    _activeGameInputLayer.DoExit();
                }
                
                _activeGameInputLayer = _lastGameInputLayer;
                
                if (_activeGameInputLayer)
                {
                    _activeGameInputLayer.DoEnter();
                    _gameInputScene.DoOnSceneLayerChanged(key, _activeGameInputLayer.key);
                }
            } else if (_activeGameInputLayer)
            {
                _activeGameInputLayer.DoUpdate();
            }
        }
        
        public void DoExit()
        {
            if (_activeGameInputLayer)
            {
                _activeGameInputLayer.DoExit();
            }
            
            //Debug.Log($"Exit on {name} {key} {title}");   
        }
        

    }
}