using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInputController : MonoBehaviour
    {
        [Header("References")] 
        [Space(10)]
        [YusamHelpBox("GameInput нужен для подключения к контролеру управления")]
        [Space(10)]
        public GameInput gameInput;
        [Space(10)]
        [YusamHelpBox("GameInputScene нужен для подключения к событию об изменении сцены и слоя")]
        [Space(10)]
        public GameInputScene gameInputScene;
        [Space(10)]
        [YusamHelpBox("Массив GameInputLayerSo нужен для проверки может ли данный скрипт упраляться в данных слоях")]
        [Space(10)]
        public GameInputLayerSo[] availableLayerSoArray;
        
        private bool _canUseGameInput;
        
        private void Awake()
        {
            if (gameInput == null)
            {
                Debug.LogError("GameInput instance not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            if (gameInputScene == null)
            {
                Debug.LogError("gameInputScene instance not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            if (gameInputScene != null)
            {
                gameInputScene.OnSceneLayerChanged += GameInputSceneOnOnSceneLayerChanged;
            }
        }

        private void GameInputSceneOnOnSceneLayerChanged(object sender, GameInputScene.OnSceneLayerChangedEventArgs e)
        {
            _canUseGameInput = false;

            foreach ( GameInputLayerSo gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo.key == e.LayerKey)
                {
                    _canUseGameInput = true;
                }
            }
        }

        public bool CanUseGameInput()
        {
            return _canUseGameInput;
        }
        
        public Vector3 GetLeftStickDirection()
        {
            Vector2 leftStickDirection = gameInput.GetLeftStickDirection();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        public Vector3 GetRightStickDirection()
        {
            Vector2 rightStickDirection = gameInput.GetRightStickDirection();
            return new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
        }
        
        private void OnDestroy()
        {
            if (gameInputScene != null)
            {
                gameInputScene.OnSceneLayerChanged -= GameInputSceneOnOnSceneLayerChanged;
            }
        }
    }
}
