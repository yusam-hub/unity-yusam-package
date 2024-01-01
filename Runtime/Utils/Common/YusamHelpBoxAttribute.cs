using UnityEngine;
using System;

namespace YusamPackage
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class YusamHelpBoxAttribute : PropertyAttribute
    {
        public string text;
        public string toolTip;
        public int rows;
        public string color;

        public YusamHelpBoxAttribute(string text, string toolTip = "", int rows = 3, string color = "#FFFFFF")
        {
            this.text = text;
            this.toolTip = toolTip;
            this.rows = rows;
            this.color = color;
        }
        
        public int lineSpace;
    }
}