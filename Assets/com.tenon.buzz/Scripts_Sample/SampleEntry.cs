using UnityEngine;
using UnityEngine.InputSystem;

public class SampleEntry : MonoBehaviour {
    private Gamepad gamepad;

    void Start() {
        TrySetCurrentGamepad();
    }

    bool TrySetCurrentGamepad() {
        if (Gamepad.current != null) {
            gamepad = Gamepad.current;
            Debug.Log("手柄已连接");

            // 测试手柄是否支持震动
            try {
                gamepad.SetMotorSpeeds(0.1f, 0.1f);
                Debug.Log("手柄支持震动");
                gamepad.SetMotorSpeeds(0, 0); // 停止震动
                return true;
            } catch (System.Exception) {
                Debug.Log("手柄不支持震动");
                return false;
            }
        } else {
            Debug.Log("没有连接手柄");
            return false;
        }
    }

    void Update() {
        if (gamepad == null) {
            var succ = TrySetCurrentGamepad();
            if (!succ) {
                return;
            }
        }

        var lowFreq = 0f;
        var highFreq = 0f;

        // 按键A，手柄小马达震动，频率低
        if (gamepad.buttonSouth.wasPressedThisFrame) {
            lowFreq = 0.25f;
            // Debug.Log("按键A：小马达低频震动");
        }

        // 按键B，手柄大马达震动，频率低
        if (gamepad.buttonEast.wasPressedThisFrame) {
            highFreq = 0.25f;
            // Debug.Log("按键B：大马达低频震动");
        }

        // 按键X，手柄小马达震动，频率高
        if (gamepad.buttonWest.wasPressedThisFrame) {
            lowFreq = 1.0f;
            // Debug.Log("按键X：小马达高频震动");
        }

        // 按键Y，手柄大马达震动，频率高
        if (gamepad.buttonNorth.wasPressedThisFrame) {
            highFreq = 1.0f;
            // Debug.Log("按键Y：大马达高频震动");
        }

        if (lowFreq > 0 || highFreq > 0) {
            gamepad.SetMotorSpeeds(lowFreq, highFreq);
        }

        // 停止震动
        if (!gamepad.buttonSouth.isPressed &&
            !gamepad.buttonEast.isPressed &&
            !gamepad.buttonWest.isPressed &&
            !gamepad.buttonNorth.isPressed) {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
}