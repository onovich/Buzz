using MortiseFrame.Swing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TenonKit.Buzz.Sample {

    public class SampleEntry : MonoBehaviour {
        Gamepad gamepad;
        RumbleCore rumbleCore;

        public RumbleTaskModel[] rumble1Task;
        public RumbleTaskModel[] rumble2Task;
        public RumbleTaskModel[] rumble3Task;
        public RumbleTaskModel[] rumble4Task;

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
            for (var i = 0; i < rumble1Task.Length; i++) {
                var task = rumble1Task[i];
                rumbleCore.CreateRumbleTaskModel(task.motorType, task.delay, task.startFreq, task.endFreq, task.duration, task.easingType, task.easingMode);
            }
        }

        void Rumble2() {
            for (var i = 0; i < rumble2Task.Length; i++) {
                var task = rumble2Task[i];
                rumbleCore.CreateRumbleTaskModel(task.motorType, task.delay, task.startFreq, task.endFreq, task.duration, task.easingType, task.easingMode);
            }
        }

        void Rumble3() {
            for (var i = 0; i < rumble3Task.Length; i++) {
                var task = rumble3Task[i];
                rumbleCore.CreateRumbleTaskModel(task.motorType, task.delay, task.startFreq, task.endFreq, task.duration, task.easingType, task.easingMode);
            }
        }

        void Rumble4() {
            for (var i = 0; i < rumble4Task.Length; i++) {
                var task = rumble4Task[i];
                rumbleCore.CreateRumbleTaskModel(task.motorType, task.delay, task.startFreq, task.endFreq, task.duration, task.easingType, task.easingMode);
            }
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