using MortiseFrame.Swing;

namespace TenonKit.Buzz.Sample {

    public static class RumbleFactory {

        public static RumbleTaskModel CreateRumbleTaskModel(MotorType motorType, float timeStamp, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            return new RumbleTaskModel {
                motorType = motorType,
                timeStamp = timeStamp,
                startFreq = startFreq,
                endFreq = endFreq,
                duration = duration,
                easingType = easingType,
                easingMode = easingMode,
            };
        }

        public static RumbleEntity CreateRumbleEntity(RumbleTaskModel model) {
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

        public static void UpdateRumbleFromModel(RumbleEntity entity, RumbleTaskModel model) {
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