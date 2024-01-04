#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

namespace YusamPackage
{
    [CustomPropertyDrawer(typeof(YusamDropdownStringAttribute), true)]
    public class YusamDropdownStringAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                /*var stringAttribute = attribute as YusamDropdownStringAttribute;
                if (stringAttribute.label != "") label.text = stringAttribute.label;*/
                
                var options = new GUIContent[]
                {
                    new GUIContent("item1"),
                    new GUIContent("default"), 
                    new GUIContent("item2")
                };

                int selectedIndex = EditorGUI.Popup(position, label, Array.IndexOf(options, property.stringValue), options);
            
                if (selectedIndex >= 0)
                {
                    //todo: not working
                    object selectedObject = options[selectedIndex].text;
                    property.SetValue(selectedObject);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);//repaint
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }        
    }
}

#endif