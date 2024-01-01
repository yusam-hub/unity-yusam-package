using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class GameInputScene : MonoBehaviour
    {
        public event EventHandler<OnSceneLayerChangedEventArgs> OnSceneLayerChanged;

        public class OnSceneLayerChangedEventArgs : EventArgs
        {
            public string SceneKey;
            public string LayerKey;
        }
        
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
        private Dictionary<string, GameInputSceneSo> _gameInputSceneDictionary;
        private GameInputSceneSo _activeGameInputScene;
        private GameInputSceneSo _lastGameInputScene;
        
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
            //Debug.Log("OnValidate");
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
                            out GameInputSceneSo gameInputScene))
                    {
                        _lastGameInputScene = gameInputScene;
                    }
                }

                if (_activeGameInputScene != null)
                {
                    _activeGameInputScene.DoEditorChangeLayerIndex(activeLayerIndex);
                }
            }
        }

        private void Awake()
        {
            _gameInputSceneDictionary = new Dictionary<string, GameInputSceneSo>();

            int index = 0;
            foreach (GameInputSceneSo gameInputSceneSo in availableGameInputSceneArray)
            {
                if (gameInputSceneSo != null)
                {
                    GameInputSceneSo temp = Instantiate(gameInputSceneSo);
                    temp.SetGameInputScene(this);
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
                if (_gameInputSceneDictionary.TryGetValue(availableGameInputSceneArray[0].key, out GameInputSceneSo gameInputScene))
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

        public void DoOnSceneLayerChanged(string sceneKey, string layerKey)
        {
            OnSceneLayerChanged?.Invoke(this, new OnSceneLayerChangedEventArgs
            {
                SceneKey = sceneKey,
                LayerKey = layerKey
            });
        }
    }
}
