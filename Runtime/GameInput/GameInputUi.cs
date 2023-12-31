using UnityEngine;

namespace YusamPackage.GameInput
{
    public class GameInputUi : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameInput gameInput;
        
        [Header("Canvas References")]
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
        
        private void Awake()
        {
            Debug.Log("Awake: " + this.name);
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy: " + this.name);
        }
        
        /*
        * UPDATE
        */
        private void Update()
        {
            if (gameInput == null) return;

            /*
             * LEFT STICK
             */
            Vector2 gameInputLeftStick = gameInput.GetLeftStickVector2Normalized();

            leftStick.leftImage.color = gameInputLeftStick.x < 0
                ? selectedColor
                : defaultColor;

            leftStick.rightImage.color = gameInputLeftStick.x > 0
                ? selectedColor
                : defaultColor;

            leftStick.topImage.color = gameInputLeftStick.y > 0
                ? selectedColor
                : defaultColor;

            leftStick.bottomImage.color = gameInputLeftStick.y < 0
                ? selectedColor
                : defaultColor;
            
            leftStick.centerImage.color = gameInput.GetLeftStickPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * RIGHT STICK
             */
            Vector2 gameInputRightStick = gameInput.GetRightStickVector2Normalized();

            rightStick.leftImage.color = gameInputRightStick.x < 0
                ? selectedColor
                : defaultColor;

            rightStick.rightImage.color = gameInputRightStick.x > 0
                ? selectedColor
                : defaultColor;

            rightStick.topImage.color = gameInputRightStick.y > 0
                ? selectedColor
                : defaultColor;

            rightStick.bottomImage.color = gameInputRightStick.y < 0
                ? selectedColor
                : defaultColor;
            
            rightStick.centerImage.color = gameInput.GetRightStickPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * LEFT PAD
             */
            leftPad.leftImage.color = gameInput.GetLeftPadLeftPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            leftPad.rightImage.color = gameInput.GetLeftPadRightPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            leftPad.topImage.color = gameInput.GetLeftPadUpPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            leftPad.bottomImage.color = gameInput.GetLeftPadDownPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * RIGHT PAD
             */
            rightPad.leftImage.color = gameInput.GetRightPadLeftPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            rightPad.rightImage.color = gameInput.GetRightPadRightPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            rightPad.topImage.color = gameInput.GetRightPadUpPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            rightPad.bottomImage.color = gameInput.GetRightPadDownPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * SELECT START
             */
            selectStart.leftImage.color = gameInput.GetSelectPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            selectStart.rightImage.color = gameInput.GetStartPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * LEFT V
             */
            leftV.topImage.color = gameInput.GetLeftBumperPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            leftV.bottomImage.color = gameInput.GetLeftTriggerPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * RIGHT V
             */
            rightV.topImage.color = gameInput.GetRightBumperPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            rightV.bottomImage.color = gameInput.GetRightTriggerPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            
            /*
             * MOUSE BUTTON
             */
            mouseButton.leftImage.color = gameInput.GetMouseLeftPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            mouseButton.centerImage.color = gameInput.GetMouseMiddlePressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
            mouseButton.rightImage.color = gameInput.GetMouseRightPressAction().IsPressed()      
                ? selectedColor
                : defaultColor;
        }
    }
}