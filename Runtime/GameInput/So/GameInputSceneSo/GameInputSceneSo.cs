using System.Collections.Generic;
using UnityEngine;

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

        public void Enter()
        {
            Debug.Log($"Enter on {name} {key} {title}");
        }

        public void Exit()
        {
            Debug.Log($"Exit on {name} {key} {title}");   
        }
    }
}