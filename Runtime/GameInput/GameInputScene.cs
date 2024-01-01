using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    public class GameInputScene : MonoBehaviour
    {
        private static GameInputScene Instance { get; set; }
        public event EventHandler<OnSceneLayerChangedEventArgs> OnSceneLayerChanged;

        public class OnSceneLayerChangedEventArgs : EventArgs
        {
            public string SceneKey;
            public string LayerKey;
        }
        [Space(10)]
        [YusamHelpBox("GameInputScene - управляет слоями доступа к GameInput, должен быть статичным и один на сцене")]   
        [Space(10)]
        [Header("References")] 
        [Space(10)]
        [YusamHelpBox("Список доступных Scriptable Objects")]
        [Space(10)]
        [SerializeField] private GameInputSceneSo[] availableGameInputSceneArray;
        [Space(10)]
        [YusamHelpBox("Можно выбирать сцену в редакторе и в игре")]
        [Space(10)]
        [YusamDropdownInt("AvailableSceneStringList()")]
        [SerializeField] private int activeSceneIndex;
        [Space(10)]
        [YusamHelpBox("Можно менять и видеть результ только в игре")]
        [Space(10)]
        [YusamDropdownInt("AvailableLayerStringList()")]
        [SerializeField] private int activeLayerIndex;
        
        [Serializable] public class SceneLayerChangedEvent : UnityEvent <string,string> {}

        [Space(20)]
        [Header("Events")] 
        [Space(10)]
        [YusamHelpBox("public void MethodName(string sceneKey, string layerKey)")]
        [Space(10)]
        [SerializeField] private SceneLayerChangedEvent sceneLayerChangedEvent = new SceneLayerChangedEvent();
        public SceneLayerChangedEvent OnSceneLayerChangedEvent { get { return sceneLayerChangedEvent; } set { sceneLayerChangedEvent = value; } }

        /*
         * PRIVATE LOCAL
         */
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

                /*
                 * todo - короче нужно сбросить activeLayerIndex при смене сцены
                 */
                if (_activeGameInputScene != null && activeLayerIndex >= 0)
                {
                    
                    _activeGameInputScene.DoEditorChangeLayerIndex(activeLayerIndex);
                }
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
                activeLayerIndex = 0;//todo - or defaultKey in layer
                
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
            sceneLayerChangedEvent?.Invoke(sceneKey, layerKey);
        }
    }
}
