using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Input/Layer")]
    public class GameInputLayerSo : ScriptableObject, IGameInputLayer
    {
        [YusamHelpBox("Слой доступа к GameInput")]
        [Space]
        public string key;
        public string title;
        public string desc;
        
        public virtual void DoEnter()
        {
            Debug.Log($"Enter on {name} {key} {title}");
        }

        public virtual void DoExit()
        {
            Debug.Log($"Exit on {name} {key} {title}");   
        }
        
        public virtual void DoUpdate()
        {

        }
    }
}