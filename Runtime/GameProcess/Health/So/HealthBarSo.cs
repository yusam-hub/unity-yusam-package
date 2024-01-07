using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Health Bar")]
    public class HealthBarSo : ScriptableObject
    {
        public HealthBarUi prefab;
        public Vector3 offset;
        public float width = 1f;
        public float height = 0.2f;
        public Color backColor = Color.red;
        public Color progressColor = Color.green;
    }
}