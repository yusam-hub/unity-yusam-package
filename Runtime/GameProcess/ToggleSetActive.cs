using UnityEngine;

namespace YusamPackage
{
    public class ToggleSetActive : MonoBehaviour
    {
        public void ExternalToggleSetActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}