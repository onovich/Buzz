using System.Collections;
using System.Collections.Generic;
using MortiseFrame.Swing;
using UnityEngine;

namespace TenonKit.Buzz.Sample {

    public class RumbleCore {

        RumbleContext ctx;

        public RumbleCore() {
            ctx = new RumbleContext();
        }

        public void Tick(float dt) {
            RumbleDomain.TickRumble(ctx, dt);
        }

        public void CreateRumbleTaskModel(MotorType motorType, float delay, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            RumbleDomain.CreateRumbleTaskModel(ctx, motorType, delay, startFreq, endFreq, duration, easingType, easingMode);
        }

        public void Clear() {
            ctx.Clear();
        }

    }

}