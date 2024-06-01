using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Buzz{

    internal struct RumbleTaskModel {

        internal float delay;
        internal MotorType motorType;
        internal float startFreq;
        internal float endFreq;
        internal float duration;
        internal EasingType easingType;
        internal EasingMode easingMode;

    }

}