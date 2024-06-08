using MortiseFrame.Swing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TenonKit.Buzz.Sample {

    public class SampleEntry : MonoBehaviour {
        Gamepad gamepad;
        RumbleCore rumbleCore;

        void Start() {
            InitRumbleCore();
            TrySetCurrentGamepad();
        }

        void InitRumbleCore() {
            rumbleCore = new RumbleCore();
            BLog.Log = Debug.Log;
            BLog.Warning = Debug.LogWarning;
            BLog.Error = Debug.LogError;
        }

        bool TrySetCurrentGamepad() {
            if (Gamepad.current != null) {
                gamepad = Gamepad.current;
                Debug.Log("手柄已连接");

                // 测试手柄是否支持震动
                try {
                    gamepad.SetMotorSpeeds(0.1f, 0.1f);
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

        void Rumble1() {
            rumbleCore.CreateRumbleTaskModel(MotorType.Left, delay: 0, startFreq: 1f, endFreq: 0f, duration: 1, EasingType.Linear, EasingMode.EaseOut);
            rumbleCore.CreateRumbleTaskModel(MotorType.Right, 0, 1f, 0f, 1, EasingType.Linear, EasingMode.EaseOut);
        }

        void Rumble2() {
            rumbleCore.CreateRumbleTaskModel(MotorType.Left, 0, 1f, 0f, 3, EasingType.Linear, EasingMode.EaseOut);
            rumbleCore.CreateRumbleTaskModel(MotorType.Right, 0, 1f, 0f, 1, EasingType.Linear, EasingMode.EaseOut);
        }

        void Rumble3() {
            rumbleCore.CreateRumbleTaskModel(MotorType.Right, 2, 0.5f, 0.5f, 1, EasingType.Linear, EasingMode.EaseOut);
            rumbleCore.CreateRumbleTaskModel(MotorType.Left, 1, 0.5f, 0.5f, 1, EasingType.Linear, EasingMode.EaseOut);
        }

        void Rumble4() {
            rumbleCore.CreateRumbleTaskModel(MotorType.Right, 0, 1f, 0f, .2f, EasingType.Sine, EasingMode.EaseOut);
            rumbleCore.CreateRumbleTaskModel(MotorType.Left, 0, 1f, 0f, 1, EasingType.Sine, EasingMode.EaseOut);
        }

        void StopAllRumble() {
            gamepad?.SetMotorSpeeds(0, 0);
            rumbleCore.Clear();
        }

        void Update() {

            if (gamepad == null) {
                return;
            }

            if (gamepad.leftTrigger.wasPressedThisFrame || gamepad.rightTrigger.wasPressedThisFrame) {
                Debug.Log("leftTrigger");
                StopAllRumble();
            }

            if (gamepad.aButton.wasPressedThisFrame) {
                Rumble1();
                Debug.Log("Rumble1");
            }

            if (gamepad.bButton.wasPressedThisFrame) {
                Rumble2();
                Debug.Log("Rumble2");
            }

            if (gamepad.xButton.wasPressedThisFrame) {
                Rumble3();
                Debug.Log("Rumble3");
            }

            if (gamepad.yButton.wasPressedThisFrame) {
                Rumble4();
                Debug.Log("Rumble4");
            }

            var dt = Time.deltaTime;
            rumbleCore.Tick(dt, out var leftFreq, out var rightFreq);
            if (gamepad != null) {
                gamepad.SetMotorSpeeds(leftFreq, rightFreq);
            }

            if (leftFreq > 0 || rightFreq > 0) {
                // Debug.Log($"leftFreq: {leftFreq}, rightFreq: {rightFreq}");
            }

        }

        void OnDestroy() {
            rumbleCore.Clear();
            StopAllRumble();
        }

        void OnApplicationQuit() {
            StopAllRumble();
        }
    }

}