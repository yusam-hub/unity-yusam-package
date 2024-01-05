using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInputController : MonoBehaviour
    {
        [Header("References")] 
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("GameInput нужен для подключения к контролеру управления")]
#endif        
        [Space(10)]
        public GameInput gameInput;
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("GameInputScene нужен для подключения к событию об изменении сцены и слоя")]
#endif        
        [Space(10)]
        public GameInputScene gameInputScene;
        [Space(10)]
#if UNITY_EDITOR        
        [YusamHelpBox("Массив GameInputLayerSo нужен для проверки может ли данный скрипт упраляться в данных слоях")]
#endif        
        [Space(10)]
        public GameInputLayerSo[] availableLayerSoArray;
        
        private bool _isLayerAccessible;
        
        private void Awake()
        {
            LogErrorHelper.NotFoundWhatInIf(gameInput == null,typeof(GameInput).ToString(), this);
            LogErrorHelper.NotFoundWhatInIf(gameInputScene == null, typeof(GameInputScene).ToString(), this);
            
            if (gameInputScene != null)
            {
                gameInputScene.OnSceneLayerChanged += GameInputSceneOnOnSceneLayerChanged;
            }
        }

        private void GameInputSceneOnOnSceneLayerChanged(object sender, GameInputScene.OnSceneLayerChangedEventArgs e)
        {
            _isLayerAccessible = false;

            foreach ( var gameInputLayerSo in availableLayerSoArray)
            {
                if (gameInputLayerSo.key == e.LayerKey)
                {
                    _isLayerAccessible = true;
                }
            }
        }

        public bool IsLayerAccessible()
        {
            return _isLayerAccessible;
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
