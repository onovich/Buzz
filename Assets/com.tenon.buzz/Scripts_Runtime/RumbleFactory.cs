using MortiseFrame.Swing;

namespace TenonKit.Buzz{

    internal static class RumbleFactory {

        internal static RumbleTaskModel CreateRumbleTaskModel(MotorType motorType, float delay, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            return new RumbleTaskModel {
                motorType = motorType,
                delay = delay,
                startFreq = startFreq,
                endFreq = endFreq,
                duration = duration,
                easingType = easingType,
                easingMode = easingMode,
            };
        }

    }

}