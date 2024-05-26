using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Buzz.Sample {

    public class RumbleEntity {

        public MotorType motorType;
        public float startFreq;
        public float endFreq;
        public float duration;
        public EasingType easingType;
        public EasingMode easingMode;

        public float currentTime;
        public bool isFinished;
        public float currentFreq;

    }

}