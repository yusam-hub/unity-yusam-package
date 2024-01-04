#if UNITY_EDITOR

using System;
using UnityEngine;

namespace YusamPackage
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class YusamDropdownIntAttribute : PropertyAttribute
    {
        public string callbackMethodAsStringList;

        public YusamDropdownIntAttribute(string callbackMethodAsStringList = "")
        {
            this.callbackMethodAsStringList = callbackMethodAsStringList;
        }
    }
}

#endif