using UnityEngine;

namespace YusamPackage
{
    public static class ApplicationHelper
    {
        public static void QuitBuildAndEditor()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
            
        }
    }
}