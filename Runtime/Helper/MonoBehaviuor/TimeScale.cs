using UnityEngine;

namespace YusamPackage
{
    public class TimeScale : MonoBehaviour
    {
        public void ExternalToggleTimeScale()
        {
            Time.timeScale = Mathf.RoundToInt(Time.timeScale) == 1 ? 0 : 1;
        }
        
        public void ExternalSetTimeScaleOne()
        {
            Time.timeScale = 1;
        }
        
        public void ExternalSetTimeScaleZero()
        {
            Time.timeScale = 0;
        }
        
        public void ExternalSetTimeScaleValue(float value)
        {
            Time.timeScale = value;
        }

    }
}