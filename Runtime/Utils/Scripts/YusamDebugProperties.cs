using System;
using UnityEngine;

namespace YusamPackage
{
    public class YusamDebugProperties : MonoBehaviour
    {
        [Space(10)]
        [YusamHelpBox("Debug Draw",1)]
        [Space(10)]
        public bool debugEnabled = true;
        public Color debugDefaultColor = Color.red;
        public float debugDefaultDuration = 0.1f;
        [Space(10)]
        [YusamHelpBox("Debug GUI",1)]
        [Space(10)]
        public bool debugDisplayEnabled = true;
        public Rect debugDisplay = new Rect(20, 20, 250, 120);
        
        void OnGUI()
        {
            if (!debugDisplayEnabled) return;
            
            GUILayout.BeginArea(debugDisplay);
            
            GUILayout.Label("Position: " + transform.position);
            
            //GUILayout.Label("Mouse Position: " + Input.mousePosition);

            /*GUILayout.Label("Label 2");
            
            GUILayout.Label("Label 3");*/

        
            GUILayout.EndArea();
        }
    }
}