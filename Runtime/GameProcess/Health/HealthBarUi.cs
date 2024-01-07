using System;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage
{
    public class HealthBarUi : MonoBehaviour
    {
        public Image backImage;
        public Image progressImage;

        public void SetHealthBarSo(HealthBarSo healthBarSo)
        {
            transform.position += healthBarSo.offset;
            backImage.color = healthBarSo.backColor;
            backImage.rectTransform.sizeDelta = new Vector2(healthBarSo.width, healthBarSo.height);
            progressImage.color = healthBarSo.progressColor;
            progressImage.rectTransform.sizeDelta = new Vector2(healthBarSo.width, healthBarSo.height);
        }

        public void SetProgress(float volume)
        {
            progressImage.fillAmount = volume;
        }
    }
}