using UnityEngine;

namespace YusamPackage
{
    public class DebugProperties : MonoBehaviour
    {
        public bool debugEnabled = true;
        public Color debugTextColor = Color.red;

        public float debugDuration = 0.1f;
        public Color debugLineColor = Color.red;
     
        public float debugLongDuration = 20f;
        public Color debugLongLineColor = Color.black;
    }
}