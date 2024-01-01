using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Layer")]
    public class GameInputLayerSo : ScriptableObject
    {
        [YusamHelpBox("Слой доступа к GameInput")]
        [Space]
        public string key;
        public string title;
        public string desc;
    }
}