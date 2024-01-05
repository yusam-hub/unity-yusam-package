using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health")]
    public class HealthSo : ScriptableObject
    {
        public float maxHealth = 100f;
        public GameObject dieEffectPrefab;
        public float dieEffectDestroyTime = 10f;
        public float dieEffectRotateAngleStartY;
        public float dieEffectRotateAngleEndY;
    }
}