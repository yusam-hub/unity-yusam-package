using System;
using UnityEngine;

namespace YusamPackage
{
    public class FlyCamera : MonoBehaviour
    {
        [Header("References")] 
        [Space(10)]
        [YusamHelpBox("GameInput нужен для подключения к контролеру управления")]
        [Space(10)]
        [SerializeField] private GameInput gameInput;
        [Space(10)]
        [YusamHelpBox("GameInputScene нужен для подключения к событию об изменении сцены и слоя")]
        [Space(10)]
        [SerializeField] private GameInputScene gameInputScene;
        [Space(10)]
        [YusamHelpBox("Массив GameInputLayerSo нужен для проверки может ли данный скрипт упраляться в данных слоях")]
        [Space(10)]
        [SerializeField] private GameInputLayerSo[] availableLayerSoArray;
        [Space(20)]

        [Header("Settings")] 
        [SerializeField] private float rotateSpeed = 50;
        [SerializeField] private float moveSpeed = 5;

        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;
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

        /**
         * GameInputSceneOnOnSceneLayerChanged
         */
        private void GameInputSceneOnOnSceneLayerChanged(object sender, GameInputScene.OnSceneLayerChangedEventArgs e)
        {
            Debug.Log($"{name} = {e.SceneKey} -> {e.LayerKey}");
            
            _canUseGameInput = false;

            foreach ( GameInputLayerSo gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo.key == e.LayerKey)
                {
                    _canUseGameInput = true;
                }
            }
        }

        /*
         * Controlled by GameInputManager
         */
        private bool IsButtonPressed()
        {
            return gameInput.GetMouseRightPressAction().IsPressed() 
                   || 
                   gameInput.GetLeftTriggerPressAction().IsPressed();
        }
        private Vector3 GetLeftStickDirection()
        {
            Vector2 leftStickDirection = gameInput.GetLeftStickVector2Normalized();
            return new Vector3(leftStickDirection.x, 0, leftStickDirection.y);
        }
        private Vector3 GetRightStickDirection()
        {
            Vector2 rightStickDirection = gameInput.GetRightStickVector2Normalized();
            return new Vector3(-rightStickDirection.y, rightStickDirection.x, 0);
        }

        /*
         * 
         */
        private void Update()
        {
            if (!_canUseGameInput) return;
            
            if (IsButtonPressed())
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UpdateMovement();
                UpdateRotation();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }  
        }
        
        private void UpdateMovement()
        {
            transform.Translate(GetLeftStickDirection() * moveSpeed * Time.deltaTime);
        }

        private void UpdateRotation()
        {
            transform.Rotate(GetRightStickDirection() * rotateSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
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