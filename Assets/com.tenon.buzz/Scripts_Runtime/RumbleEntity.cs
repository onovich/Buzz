using MortiseFrame.Swing;

namespace TenonKit.Buzz{

    internal class RumbleEntity {

        internal MotorType motorType;
        internal float startFreq;
        internal float endFreq;
        internal float duration;
        internal EasingType easingType;
        internal EasingMode easingMode;

        internal float currentTime;
        internal bool isFinished;
        internal float currentFreq;

    }

}