#if UNITY_EDITOR

using System;
using UnityEngine;

namespace YusamPackage
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class YusamDropdownStringAttribute : PropertyAttribute
    {
        public string label;

        public YusamDropdownStringAttribute(string label = "")
        {
            this.label = label;
        }
    }
}

#endif