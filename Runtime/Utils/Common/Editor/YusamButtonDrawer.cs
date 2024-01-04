using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace YusamPackage
{
    [CustomPropertyDrawer(typeof(YusamButtonAttribute))]
    public class SingletonEditor : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            YusamButtonAttribute target = (YusamButtonAttribute)attribute;

            if (target != null)
            {
                GUI.enabled = target.enabledJustInPlayMode && Application.isPlaying || !target.enabledJustInPlayMode;
                Rect rect = position;
                rect.height = 20;
                if (GUI.Button(rect, new GUIContent(target.label, 
                        GUI.enabled ? "Call function " + target.function : "Enabled Just in Play Mode")))
                {
                    ExecuteFunction(target);
                }
                GUI.enabled = true;
            }

        }

        public override float GetHeight()
        {
            return 30f;
        }

        void ExecuteFunction(YusamButtonAttribute target)
        {
            if (target.type == null) return;
            
            UnityEngine.Object theObject = Selection.activeGameObject.GetComponent(target.type) as UnityEngine.Object;

            MethodInfo tMethod = theObject.GetType().GetMethods().FirstOrDefault(
                method => method.Name == target.function && method.GetParameters().Count() == 0
                );

            if (tMethod != null)
            {
                tMethod.Invoke(theObject,null);
            }
        }
    }
}

