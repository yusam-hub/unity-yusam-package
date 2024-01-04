using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Scene")]
    public class GameInputSceneSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR
        [YusamHelpBox("Список слоев для GameInput")]
#endif        
        public GameInputLayerSo[] availableLayerSoArray;
        [Space(10)]
        public string defaultLayerKey;
        [Space(20)]
#if UNITY_EDITOR        
        [YusamHelpBox("Settings")]
#endif        
        [Space(10)]
        public string key;
        public string title;
        public string desc;


        private List<String> _availableList = new List<string>();
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

        public void DoEditorChangeLayerIndex(int index)
        {
            //Debug.Log($"DoEditorChangeLayerIndex on {name} {key} {title}");
            if (_gameInputLayerDictionary != null && _gameInputLayerDictionary.Count > 0)
            {
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[index].key, out GameInputLayerSo gameInputLayer))
                {
                    _lastGameInputLayer = gameInputLayer;
                }
            }
        }
        
        public virtual void DoEnter()
        {
            //Debug.Log($"Enter on {name} {key} {title}");
            
            StoreLayerEditorChanged();
            
            if (_gameInputLayerDictionary == null)
            {
                _gameInputLayerDictionary = new Dictionary<string, GameInputLayerSo>();
            }
            else
            {
                foreach (KeyValuePair<string, GameInputLayerSo> p in _gameInputLayerDictionary)
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
                    GameInputLayerSo temp = Instantiate(gameInputLayerSo);
                    //temp - can doit somethis
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
                if (_gameInputLayerDictionary.TryGetValue(availableLayerSoArray[0].key, out GameInputLayerSo gameInputLayer))
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
                    _gameInputScene.DoOnSceneLayerChanged(key, _activeGameInputLayer.key);
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
            
            //Debug.Log($"Exit on {name} {key} {title}");   
        }
        

    }
}