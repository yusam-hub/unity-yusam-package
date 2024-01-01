using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Scene")]
    public class GameInputSceneSo : ScriptableObject
    {
        [YusamHelpBox("Слои на сцене для GameInput")]
        [Space]
        public string key;
        public string title;
        public string desc;
        public GameInputLayerSo[] availableLayerSoArray;
        public string defaultSceneKey;
    }
}