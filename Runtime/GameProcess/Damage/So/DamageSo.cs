﻿using UnityEngine;

namespace YusamPackage
{
    [CreateAssetMenu(menuName = "So/Yusam Package/Game Process/Damage")]
    public class DamageSo : ScriptableObject
    {
        [Space(10)]
        [YusamHelpBox("Эффект при попадании", 1)]
        [Space(10)]
        public GameObject hitEffectPrefab;
        
        [Space(10)]
        [YusamHelpBox("Проложительность жизни скрипта эффекта", 2)]
        [Space(10)]
        public float hitEffectDestroyTime = 1f;
    }
}