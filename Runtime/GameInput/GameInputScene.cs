using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInputScene : MonoBehaviour
    {
        public static GameInputScene Instance { get; private set; }
        public event EventHandler<OnSceneLayerChangedEventArgs> OnSceneLayerChanged;

        public class OnSceneLayerChangedEventArgs : EventArgs
        {
            public string SceneKey;
            public string LayerKey;
        }
        [SerializeField] private GameInputSceneSo[] availableGameInputSceneArray;
        [SerializeField] private int activeSceneIndex;
        [SerializeField] private int activeLayerIndex;
        
        [Serializable] public class SceneLayerChangedEvent : UnityEvent <string,string> {}
        [SerializeField] private SceneLayerChangedEvent sceneLayerChangedEvent = new();
        public SceneLayerChangedEvent OnSceneLayerChangedEvent { get { return sceneLayerChangedEvent; } set { sceneLayerChangedEvent = value; } }

        /*
         * PRIVATE LOCAL
         */
        private readonly List<String> _availableList = new();
        private Dictionary<string, GameInputSceneSo> _gameInputSceneDictionary;
        private GameInputSceneSo _activeGameInputScene;
        private GameInputSceneSo _lastGameInputScene;
        
        public void StoreSceneEditorChanged()
        {
            _availableList.Clear();

            if (availableGameInputSceneArray != null)
            {
                foreach (var gameInputSceneSo in availableGameInputSceneArray)
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

        public void SetActiveLayerIndex(int value)
        {
            activeLayerIndex = value;
            if (_activeGameInputScene != null && activeLayerIndex >= 0)
            {
                    
                _activeGameInputScene.DoEditorChangeLayerIndex(activeLayerIndex);
            }
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
                            out var gameInputScene))
                    {
                        _lastGameInputScene = gameInputScene;
                    }
                }

                /*
                 * todo - короче нужно сбросить activeLayerIndex при смене сцены
                 */
                SetActiveLayerIndex(activeLayerIndex);
            }
        }

        private void Awake()
        {
            
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _gameInputSceneDictionary = new Dictionary<string, GameInputSceneSo>();

            int index = 0;
            foreach (var gameInputSceneSo in availableGameInputSceneArray)
            {
                if (gameInputSceneSo)
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

            if (!_lastGameInputScene && _gameInputSceneDictionary.Count > 0)
            {
                if (_gameInputSceneDictionary.TryGetValue(availableGameInputSceneArray[0].key, out var gameInputScene))
                {
                    _lastGameInputScene = gameInputScene;
                }
            }
        }

        private void Update()
        {
            if (_activeGameInputScene != _lastGameInputScene)
            {
                if (_activeGameInputScene)
                {
                    _activeGameInputScene.DoExit();
                }
                
                _activeGameInputScene = _lastGameInputScene;
                activeLayerIndex = 0;//todo - or defaultKey in layer
                
                if (_activeGameInputScene)
                {
                    _activeGameInputScene.DoEnter();
                }
            } else if (_activeGameInputScene)
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
            sceneLayerChangedEvent?.Invoke(sceneKey, layerKey);
        }
    }
}
