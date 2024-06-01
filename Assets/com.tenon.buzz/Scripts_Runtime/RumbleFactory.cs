using MortiseFrame.Swing;

namespace TenonKit.Buzz.Sample {

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

        internal static RumbleEntity CreateRumbleEntity(RumbleTaskModel model) {
            return new RumbleEntity {
                motorType = model.motorType,
                startFreq = model.startFreq,
                endFreq = model.endFreq,
                duration = model.duration,
                easingType = model.easingType,
                easingMode = model.easingMode,

                currentTime = 0,
                isFinished = false,
                currentFreq = model.startFreq,
            };
        }

        internal static void UpdateRumbleFromModel(RumbleEntity entity, RumbleTaskModel model) {
            entity.motorType = model.motorType;
            entity.startFreq = model.startFreq;
            entity.endFreq = model.endFreq;
            entity.duration = model.duration;
            entity.easingType = model.easingType;
            entity.easingMode = model.easingMode;

            entity.currentTime = 0;
            entity.isFinished = false;
            entity.currentFreq = model.startFreq;
        }

    }

}