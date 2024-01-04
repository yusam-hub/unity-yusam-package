
using UnityEngine;
using UnityEditor;
using System;

namespace YusamPackage
{
    [CustomPropertyDrawer(typeof(YusamDropdownBoolAttribute), true)]
    public class YusamDropdownBoolAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                var boolAttribute = attribute as YusamDropdownBoolAttribute;
                if (boolAttribute.label != "") label.text = boolAttribute.label;
                var options = new GUIContent[]
                {
                    new GUIContent(boolAttribute.falseValue), 
                    new GUIContent(boolAttribute.trueValue)
                };
                property.boolValue =
                    Convert.ToBoolean(EditorGUI.Popup(position, label, Convert.ToInt32(property.boolValue), options));
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}