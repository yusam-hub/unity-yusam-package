using UnityEngine;
using UnityEngine.InputSystem;

namespace YusamPackage.GameInput
{
    public interface IGameInputManager
    {
        /*
         * KEYBOARD + GAMEPAD
         */
        public Vector2 GetLeftStickVector2Normalized();
        public Vector2 GetRightStickVector2Normalized();
        
        public InputAction GetLeftTriggerPressAction();
        public InputAction GetLeftBumperPressAction();
        public InputAction GetRightTriggerPressAction();
        public InputAction GetRightBumperPressAction();
        public InputAction GetLeftStickPressAction();
        public InputAction GetRightStickPressAction();
        public InputAction GetLeftPadLeftPressAction();
        public InputAction GetLeftPadRightPressAction();
        public InputAction GetLeftPadUpPressAction();
        public InputAction GetLeftPadDownPressAction();
        public InputAction GetRightPadLeftPressAction();
        public InputAction GetRightPadRightPressAction();
        public InputAction GetRightPadUpPressAction();
        public InputAction GetRightPadDownPressAction();
        public InputAction GetStartPressAction();
        public InputAction GetSelectPressAction();

        /*
         * MOUSE
         */
        public Vector2 GetMouseNormalized();
        public InputAction GetMouseLeftPressAction();
        public InputAction GetMouseMiddlePressAction();
        public InputAction GetMouseRightPressAction();
    }
}