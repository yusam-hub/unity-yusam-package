using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Shield")]
    public class ShieldSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Проложительность жизни объекта эффекта", 2)]
        [Space(10)]
        public float prefabLifeTime = 5f;
        
    }
}