using UnityEngine;

namespace YusamPackage.GameInput
{
    public class GameInputUi : MonoBehaviour
    {
        [Header("Canvas UI")]
        [SerializeField] public GameInputControlPanelUi leftV;
        [SerializeField] public GameInputControlPanelUi leftStick;
        [SerializeField] public GameInputControlPanelUi leftPad;
        [SerializeField] public GameInputControlPanelUi selectStart;
        [SerializeField] public GameInputControlPanelUi rightStick;
        [SerializeField] public GameInputControlPanelUi rightPad;
        [SerializeField] public GameInputControlPanelUi rightV;
        [SerializeField] public GameInputControlPanelUi mouseButton;
     
        [Header("Colors")]
        [SerializeField] public Color defaultColor = Color.white;
        [SerializeField] public Color selectedColor = Color.gray;
    }
}