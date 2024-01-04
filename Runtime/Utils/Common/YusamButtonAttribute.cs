using System;
using UnityEngine;

namespace YusamPackage
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class YusamButtonAttribute : PropertyAttribute
    {
        public readonly string label;
        public readonly string function;
        public readonly int id;
        public readonly Type type;
        public readonly bool enabledJustInPlayMode;

        public YusamButtonAttribute(string label, string function, Type type, bool enabledJustInPlayMode = true)
        {
            this.label = label;
            this.function = function;
            this.type = type;
            this.enabledJustInPlayMode = enabledJustInPlayMode;
        }
    }
}

