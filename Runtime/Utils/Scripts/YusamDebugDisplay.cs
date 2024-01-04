using System;
using JetBrains.Annotations;
using UnityEngine;

namespace YusamPackage
{
    /*
     * USAGE:
     *      YusamDebugDisplay.HasInstance()
     *      YusamDebugDisplay.Instance.DisplayFirst($"Debug: {name}");
     *      YusamDebugDisplay.Instance.DisplayLast($"Debug: {name}");
     *      YusamDebugDisplay.Instance.DisplayLog(10,$"Debug: {name}");     
     */
    [DisallowMultipleComponent]
    [RequireComponent(typeof(YusamDebugProperties))]
    public class YusamDebugDisplay : MonoBehaviour
    {
        public const int GAME_INPUT_GET_MOUSE_POSITION = -3;
        public const int GAME_INPUT_GET_VIRTUAL_MOUSE_POSITION = -2;
        public const int GAME_INPUT_GET_RIGHT_STICK_MOUSE_POSITION = -1;
        
        [SerializeField] private int countLines = 42;
        
        private string[] _displayLines; 
        
        public static YusamDebugDisplay Instance { get; private set; }

        private YusamDebugProperties _yusamDebugProperties;
        
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
            
            _yusamDebugProperties = GetComponent<YusamDebugProperties>();

            _displayLines = new string[countLines];
        }

        public void DisplayLog(int index, string lineString)
        {
            if (index < 0)
            {
                index = _displayLines.Length + index;
            }

            if (index < 0) return;
            if (index > _displayLines.Length - 1) return;
            
            _displayLines[index] = $"{index} - {lineString}";
        }
        
        public void DisplayFirst(int index, string lineString)
        {
            DisplayLog(0, lineString);
        }
        
        public void DisplayLast(int index, string lineString)
        {
            DisplayLog(_displayLines.Length-1, lineString);
        }

        public bool DebugEnabled()
        {
            return _yusamDebugProperties.debugEnabled;
        }

        public void SetEmptyForAll()
        {
            for (int i = 0; i < _displayLines.Length; i++)
            {
                _displayLines[i] = "";
            }
        }
        
        private void OnGUI()
        {
            if (!_yusamDebugProperties.debugEnabled) return;       
            
            GUILayout.BeginArea(new Rect(10,10, Screen.width-20, Screen.height-20));
            
            GUIStyle style = GUI.skin.label;
            style.fontSize = 12;
            style.normal.textColor = _yusamDebugProperties.debugTextColor;
            style.active.textColor = _yusamDebugProperties.debugTextColor;
            style.hover.textColor = _yusamDebugProperties.debugTextColor;
            style.focused.textColor = _yusamDebugProperties.debugTextColor;

            foreach (string value in _displayLines)
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