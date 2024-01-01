using UnityEditor;
using UnityEngine;

namespace YusamPackage
{
    [CustomPropertyDrawer(typeof(YusamHelpBoxAttribute))]
    public class YusamHelpBoxDecorator : DecoratorDrawer
    {
        YusamHelpBoxAttribute _helpBox;
        GUIContent _content;

        public YusamHelpBoxAttribute helpBox
        {
            get
            {
                if (_helpBox == null)
                {
                    _helpBox = (YusamHelpBoxAttribute)attribute;
                }

                return _helpBox;
            }
        }

        public GUIContent content
        {
            get
            {
                if (_content == null)
                {
                    _content = new GUIContent(helpBox.text);
                }

                return _content;
            }
        }
        
        public override void OnGUI(Rect position)
        {
            content.text = helpBox.text;
            
            GUIStyle style = new GUIStyle(EditorStyles.helpBox);
            style.wordWrap = true;
            style.richText = true;
            style.fontSize = 12;  
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter; 
            style.normal.textColor = Color.white;
            style.hover.textColor = Color.white;
            style.active.textColor = Color.white;
            style.focused.textColor = Color.white;

            if (ColorUtility.TryParseHtmlString(helpBox.color, out Color newCol))
            {
                style.normal.textColor = newCol;
                style.hover.textColor = newCol;
                style.active.textColor = newCol;
                style.focused.textColor = newCol;
            }
            
            GUI.Box(position, content, style);
        }
        
        public override float GetHeight()
        {
            return base.GetHeight() * helpBox.rows; 
        }
    }
}