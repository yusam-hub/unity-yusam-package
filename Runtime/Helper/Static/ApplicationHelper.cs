namespace YusamPackage
{
    public static class ApplicationHelper
    {
        public static void QuitBuildAndEditor()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else            
            Application.Quit();
#endif
        }
    }
}