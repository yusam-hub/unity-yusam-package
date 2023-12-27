using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage.GameDebug
{
    public class GameDebugCellTemplate : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshProUGUI cellText;


        public Image GetBackgroundImage()
        {
            return backgroundImage;
        }
        
        public TextMeshProUGUI GetCellText()
        {
            return cellText;
        }

        public void SetCellText(string text)
        {
            cellText.text = text;

            if (cellText.text == "")
            {
                backgroundImage.color = new Color(255, 255, 255, 0);
            }
            else
            {
                backgroundImage.color = new Color(255, 255, 255, 0);
            }
        }
    }
}
