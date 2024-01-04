using UnityEngine;

namespace YusamPackage
{
    public class GameMenuItemsUi : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log($"{GetType()} - Awake");
        }

        private void OnDestroy()
        {
            Debug.Log($"{GetType()} - OnDestroy");
        }
    }
}
