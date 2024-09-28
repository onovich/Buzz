using System;
using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Buzz{

    [Serializable]
    public struct RumbleTaskModel {

        public MotorType motorType;
        public float delay;
        public float startFreq;
        public float endFreq;
        public float duration;
        public EasingType easingType;
        public EasingMode easingMode;

    }

}