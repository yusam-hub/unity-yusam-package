using UnityEngine;

namespace YusamPackage
{
    public class GameMenuItemsUi : MonoBehaviour
    {
        private void Awake()
        {
            GameDebug.Log("Awake: " + this.name);
        }

        private void OnDestroy()
        {
            GameDebug.Log("OnDestroy: " + this.name);
        }
    }
}
