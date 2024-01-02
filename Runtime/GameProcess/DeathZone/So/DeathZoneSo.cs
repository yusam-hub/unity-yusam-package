using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Death Zone")]
    public class DeathZoneSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Кол-во повреждений", 2)]
        [Space(10)]
        public float damage = 1f;
    }
}