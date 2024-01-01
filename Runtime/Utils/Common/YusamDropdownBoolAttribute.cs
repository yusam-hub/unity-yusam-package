using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    /*
     *      [YusamDropdownBool("", "No", "Yes")]
     *      [SerializeField] private bool isTest;
     */
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class YusamDropdownBoolAttribute : PropertyAttribute
    {
        public string label, falseValue, trueValue;

        public YusamDropdownBoolAttribute(string label = "", string falseValue = "No", string trueValue = "Yes")
        {
            this.label = label;
            this.falseValue = falseValue;
            this.trueValue = trueValue;
        }
    }
}
