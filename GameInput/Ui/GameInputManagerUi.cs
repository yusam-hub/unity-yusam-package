using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameInput.Ui
{
    public class GameInputManagerUi : MonoBehaviour
    {
        [Header("Canvas UI")]
        [SerializeField] public GameInputManagerControlPanelUi leftV;
        [SerializeField] public GameInputManagerControlPanelUi leftStick;
        [SerializeField] public GameInputManagerControlPanelUi leftPad;
        [SerializeField] public GameInputManagerControlPanelUi selectStart;
        [SerializeField] public GameInputManagerControlPanelUi rightStick;
        [SerializeField] public GameInputManagerControlPanelUi rightPad;
        [SerializeField] public GameInputManagerControlPanelUi rightV;
     
        [Header("Colors")]
        [SerializeField] public Color defaultColor = Color.white;
        [SerializeField] public Color selectedColor = Color.gray;
    }
}