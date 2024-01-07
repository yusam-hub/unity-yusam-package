using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Destroyable")]
    public class DestroyableSo : ScriptableObject
    {
        public int destroyBonus = 5;
        public GameObject prefabOnSelfDestroy;
        public float prefabLifeTime = 5f;
    }
}