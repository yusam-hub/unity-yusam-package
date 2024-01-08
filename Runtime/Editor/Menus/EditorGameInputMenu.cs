#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace YusamPackage
{
    public static class EditorGameInputMenu
    {
        const string m_YusamPackageAssetsRootMenu = "Assets/Yusam Package/Game Input/";
        const string m_YusamPackageGameObjectRootMenu = "GameObject/Yusam Package/Game Input/";
        const int m_GameObjectMenuPriority = 0;
        
        [MenuItem(m_YusamPackageGameObjectRootMenu + "Game Input", false, m_GameObjectMenuPriority)]
        public static void CreateGameInput(MenuCommand menuCommand)
        {
            UtilityHelper.PlaceIntoEditScene(
                UtilityHelper.CreateObject("GameInput", typeof(GameInput))
            );
        }

        [MenuItem(m_YusamPackageGameObjectRootMenu + "Game Input Cursor", false, m_GameObjectMenuPriority)]
        public static void CreateGameInputCursor(MenuCommand menuCommand)
        {
            UtilityHelper.PlaceIntoEditScene(
                UtilityHelper.CreateObject("GameInputCursor", typeof(GameInputCursor))
            );
        }

        
        /*[MenuItem(m_YusamPackageAssetsRootMenu + "Game Input Prefab", false, m_GameObjectMenuPriority)]
        public static void CreateGameInputPrefab(MenuCommand menuCommand)
        {
            UtilityHelper.CreatePrefab("Resources/Env/Prefabs/GameInput/GameInput");
        }*/
    }
}
#endif