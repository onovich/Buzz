using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Buzz.Sample {

    public struct RumbleTaskModel {

        public float timeStamp;
        public MotorType motorType;
        public float startFreq;
        public float endFreq;
        public float duration;
        public EasingType easingType;
        public EasingMode easingMode;

    }

}