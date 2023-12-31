using System;
using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Menu")]
    public class GameMenuSo : ScriptableObject
    {
        [Serializable]
        public struct GameMenuStruct
        {
            public string menuKey;
            public string menuText;
        }
        
        public GameMenuStruct[] gameMenuStructArray;
    }
}
