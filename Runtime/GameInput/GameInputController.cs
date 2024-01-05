using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class GameInputController : MonoBehaviour
    {
        public GameInput gameInput;
        public GameInputScene gameInputScene;
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
