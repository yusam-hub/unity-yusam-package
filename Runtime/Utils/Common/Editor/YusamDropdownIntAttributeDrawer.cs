using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


namespace YusamPackage
{
    /*
     * [YusamDropdownInt("AvailableSceneStringList()")]
     */
    [CustomPropertyDrawer(typeof(YusamDropdownIntAttribute), true)]
    public class YusamDropdownIntAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                var intAttribute = attribute as YusamDropdownIntAttribute;

                if (intAttribute.callbackMethodAsStringList != "")
                {

                    object baseMaster = property.serializedObject.targetObject;
                    object obj = ReflectionSystem.GetValue(baseMaster, intAttribute.callbackMethodAsStringList); //get the list from path

                    GUIContent[] options;
                    if (obj.GetType().IsGenericType && obj.GetType().GetGenericTypeDefinition() == typeof(List<>))
                    {
                        List<string> list = obj as List<string>;
                        int c = list.Count;
                        options = new GUIContent[c];
                        for (int i = 0; i < c; i++)
                        {
                            options[i] = new GUIContent(list[i]);      
                        }
                    }
                    else
                    {
                        options = new GUIContent[]
                        {
                        };
                    }

                    property.intValue = Convert.ToInt32(EditorGUI.Popup(position, label, Convert.ToInt32(property.intValue), options));
                    
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
   
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}