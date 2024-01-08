#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace YusamPackage
{
    public static class EditorGameProcessMenu
    {
        //const string m_YusamPackageAssetsRootMenu = "Assets/Yusam Package/Game Process/";
        const string m_YusamPackageGameObjectRootMenu = "GameObject/Yusam Package/Game Process/";
        const int m_GameObjectMenuPriority = 0;
        
        [MenuItem(m_YusamPackageGameObjectRootMenu + "Experience", false, m_GameObjectMenuPriority)]
        public static void CreateExperience(MenuCommand menuCommand)
        {
            UtilityHelper.PlaceIntoEditScene(
                UtilityHelper.CreateObject("Experience", typeof(Experience))
            );
        }
    }
}
#endif