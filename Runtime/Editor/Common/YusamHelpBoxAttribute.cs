#if UNITY_EDITOR

using UnityEngine;
using System;

namespace YusamPackage
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class YusamHelpBoxAttribute : PropertyAttribute
    {
        public string text;
        public int rows;
        public string color;
        public int fontSize;

        public YusamHelpBoxAttribute(string text, int rows = 3, string color = "#FF8000", int fontSize = 12)
        {
            this.text = text;
            this.rows = rows;
            this.color = color;
            this.fontSize = fontSize;
        }

    }
}

#endif