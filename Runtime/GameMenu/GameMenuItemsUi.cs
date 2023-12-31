using UnityEngine;

namespace YusamPackage
{
    public class GameMenuItemsUi : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake: " + this.name);
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy: " + this.name);
        }
    }
}
