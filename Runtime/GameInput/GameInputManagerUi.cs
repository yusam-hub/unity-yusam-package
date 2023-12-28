using UnityEngine;

namespace YusamPackage.GameInput
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
        [SerializeField] public GameInputManagerControlPanelUi mouseButton;
     
        [Header("Colors")]
        [SerializeField] public Color defaultColor = Color.white;
        [SerializeField] public Color selectedColor = Color.gray;
    }
}