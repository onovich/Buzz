using System;
using MortiseFrame.Swing;

namespace TenonKit.Buzz {

    internal class RumbleEntity {

        internal float startFreq;
        internal float endFreq;
        internal float duration;
        internal EasingType easingType;
        internal EasingMode easingMode;

        internal float currentTime;
        internal bool isFinished;

        internal float currentFreq;

        internal void UpdateRumbleFromModel(RumbleTaskModel model) {
            this.startFreq = model.startFreq;
            this.endFreq = model.endFreq;
            this.duration = model.duration;
            this.easingType = model.easingType;
            this.easingMode = model.easingMode;

            this.currentTime = 0;
            this.isFinished = false;
            this.currentFreq = model.startFreq;
        }

    }

}