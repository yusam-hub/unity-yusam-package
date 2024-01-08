using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace YusamPackage
{
    public static class UtilityHelper
    {
        public static GameObject CreatePrefab(string path)
        {
            GameObject newObject = PrefabUtility.InstantiatePrefab(Resources.Load(path) ) as GameObject;
            //PlaceIntoEditScene(newObject);
            return newObject;
        }
        
        public static GameObject CreateObject(string name, params Type[] types)
        {
            GameObject newObject = ObjectFactory.CreateGameObject(name, types);
            //PlaceIntoEditScene(newObject);
            return newObject;
        }

        public static GameObject PlaceIntoEditScene(GameObject gameObject)
        {
            SceneView lastView = SceneView.lastActiveSceneView;
            gameObject.transform.position = lastView ? lastView.pivot : Vector3.zero;

            StageUtility.PlaceGameObjectInCurrentStage(gameObject);
            GameObjectUtility.EnsureUniqueNameForSibling(gameObject);
            
            Undo.RegisterCreatedObjectUndo(gameObject, $"Create Object: {gameObject.name}");
            Selection.activeGameObject = gameObject;
            Selection.activeGameObject.transform.position = Vector3.zero;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            return gameObject;
        }

    }
}