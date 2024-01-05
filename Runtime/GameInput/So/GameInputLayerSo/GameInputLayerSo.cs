using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Layer")]
    public class GameInputLayerSo : ScriptableObject
    {
        public string key;
        public string title;
        
        public void DoEnter()
        {
            //Debug.Log($"Enter on {name} {key} {title}");
        }

        public void DoExit()
        {
            //Debug.Log($"Exit on {name} {key} {title}");   
        }
        
        public void DoUpdate()
        {

        }
    }
}