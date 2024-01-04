﻿using System;
using UnityEngine;

namespace YusamPackage
{
    public class YusamDebugProperties : MonoBehaviour
    {
        [Space(10)]
        [YusamHelpBox("Debug Draw",1)]
        [Space(10)]
        public bool debugEnabled = true;
        public Color debugLineColor = Color.red;
        public Color debugTextColor = Color.red;
        public float debugDuration = 0.1f;
    }
}