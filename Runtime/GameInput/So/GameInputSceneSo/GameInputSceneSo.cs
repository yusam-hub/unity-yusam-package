using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Scene")]
    public class GameInputSceneSo : ScriptableObject, IGameInputScene
    {
        [YusamHelpBox("Слои на сцене для GameInput")]
        [Space]
        public string key;
        public string title;
        public string desc;
        public GameInputLayerSo[] availableLayerSoArray;
        public string defaultLayerKey;

        private List<String> _availableList = new List<string>();
        private Dictionary<string, IGameInputLayer> _gameInputLayerDictionary;
        private IGameInputLayer _activeGameInputLayer;
        private IGameInputLayer _lastGameInputLayer;
        
        public void StoreLayerEditorChanged()
        {
            _availableList.Clear();
            foreach (GameInputLayerSo gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo != null)
                {
                    _availableList.Add(gameInputLayerSo.title);
                }
            }
        }
        
        public List<string> AvailableLayerStringList()
        {
            return _availableList;
        }

        public virtual void DoEditorChangeLayerIndex(int index)
        {
            //Debug.Log($"DoEditorChangeLayerIndex on {name} {key} {title}");
            if (_gameInputLayerDictionary != null && _gameInputLayerDictionary.Count > 0)
            {
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[index].key, out IGameInputLayer gameInputLayer))
                {
                    _lastGameInputLayer = gameInputLayer;
                }
            }
        }
        
        public virtual void DoEnter()
        {
            Debug.Log($"Enter on {name} {key} {title}");
            
            StoreLayerEditorChanged();
            
            if (_gameInputLayerDictionary == null)
            {
                _gameInputLayerDictionary = new Dictionary<string, IGameInputLayer>();
            }
            else
            {
                foreach (KeyValuePair<string, IGameInputLayer> p in _gameInputLayerDictionary)
                {
                    Object temp = p.Value as Object;
                    Destroy(temp);
                }

                _gameInputLayerDictionary.Clear();
            }


            int index = 0;
            foreach (GameInputLayerSo gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo != null)
                {
                    IGameInputLayer temp = Instantiate(gameInputLayerSo);
                    _gameInputLayerDictionary.Add(gameInputLayerSo.key, temp);
                    
                    if (defaultLayerKey == gameInputLayerSo.key)
                    {
                        _lastGameInputLayer = temp;
                    }

                    index++;
                }
            }

            if (_lastGameInputLayer == null && _gameInputLayerDictionary.Count > 0)
            {
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[0].key, out IGameInputLayer gameInputLayer))
                {
                    _lastGameInputLayer = gameInputLayer;
                }
            }
            
        }

        public virtual void DoUpdate()
        {
            if (_activeGameInputLayer != _lastGameInputLayer)
            {
                if (_activeGameInputLayer != null)
                {
                    _activeGameInputLayer.DoExit();
                }
                
                _activeGameInputLayer = _lastGameInputLayer;
                
                if (_activeGameInputLayer != null)
                {
                    _activeGameInputLayer.DoEnter();
                }
            } else if (_activeGameInputLayer != null)
            {
                _activeGameInputLayer.DoUpdate();
            }
        }
        
        public virtual void DoExit()
        {
            if (_activeGameInputLayer != null)
            {
                _activeGameInputLayer.DoExit();
            }
            
            Debug.Log($"Exit on {name} {key} {title}");   
        }
        

    }
}