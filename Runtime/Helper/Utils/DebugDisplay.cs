﻿using UnityEngine;

namespace YusamPackage
{
    /*
     * USAGE:
     *      DebugDisplay.HasInstance()
     *      DebugDisplay.Instance.DisplayFirst($"Debug: {name}");
     *      DebugDisplay.Instance.DisplayLast($"Debug: {name}");
     *      DebugDisplay.Instance.DisplayIndex(10,$"Debug: {name}");     
     */
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DebugProperties))]
    public class DebugDisplay : MonoBehaviour
    {

        public const int GAME_INPUT_GET_MOUSE_POSITION = -4;
        public const int GAME_INPUT_GET_VIRTUAL_MOUSE_POSITION = -3;
        public const int GAME_INPUT_GET_RIGHT_STICK_MOUSE_POSITION = -2;
        
        [SerializeField] private int fontSize = 12;
        [SerializeField] private int countLines = 42;
        
        private string[] _displayLines; 
        
        public static DebugDisplay Instance { get; private set; }

        private DebugProperties _debugProperties;
        
        public static bool HasInstance()
        {
            return Instance;
        }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance);
            }
            Instance = this;
            
            _debugProperties = GetComponent<DebugProperties>();

            _displayLines = new string[countLines];
        }

        public void DisplayIndex(int index, string lineString)
        {
            if (index < 0)
            {
                index = _displayLines.Length + index;
            }

            if (index < 0) return;
            if (index > _displayLines.Length - 1) return;
            
            _displayLines[index] = $"{index} - {lineString}";
        }
        
        public void DisplayFirst(string lineString)
        {
            DisplayIndex(0, lineString);
        }
        
        public void DisplayLast(string lineString)
        {
            DisplayIndex(_displayLines.Length-1, lineString);
        }

        public bool DebugEnabled()
        {
            return _debugProperties.debugEnabled;
        }

        public void SetEmptyForAll()
        {
            for (var i = 0; i < _displayLines.Length; i++)
            {
                _displayLines[i] = "";
            }
        }
        
        private void OnGUI()
        {
            if (!_debugProperties.debugEnabled) return;       
            
            GUILayout.BeginArea(new Rect(10,10, Screen.width-20, Screen.height-20));
            
            var style = GUI.skin.label;
            style.fontSize = fontSize;
            style.normal.textColor = _debugProperties.debugTextColor;
            style.active.textColor = _debugProperties.debugTextColor;
            style.hover.textColor = _debugProperties.debugTextColor;
            style.focused.textColor = _debugProperties.debugTextColor;

            foreach (var value in _displayLines)
            {
                if (value != "")
                {
                    GUILayout.Label(value, style);
                }
            }
            GUILayout.EndArea();
        }
    }
}