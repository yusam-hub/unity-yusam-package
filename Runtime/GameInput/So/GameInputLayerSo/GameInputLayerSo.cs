﻿using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Layer")]
    public class GameInputLayerSo : ScriptableObject
    {
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Слой доступа к GameInput")]
#endif        
        [Space(10)]
        public string key;
        public string title;
        
        public virtual void DoEnter()
        {
            //Debug.Log($"Enter on {name} {key} {title}");
        }

        public virtual void DoExit()
        {
            //Debug.Log($"Exit on {name} {key} {title}");   
        }
        
        public virtual void DoUpdate()
        {

        }
    }
}