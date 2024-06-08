using MortiseFrame.Swing;
using UnityEngine.UIElements.Experimental;

namespace TenonKit.Buzz {

    internal static class RumbleDomain {

        internal static void CreateRumbleTaskModel(RumbleContext ctx, MotorType motorType, float delay, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            if (motorType == MotorType.None) {
                BLog.Error("RumbleCore " + "CreateRumbleTaskModel " + "motorType is not valid");
                return;
            }
            var model = RumbleFactory.CreateRumbleTaskModel(motorType, delay, startFreq, endFreq, duration, easingType, easingMode);
            ctx.AddTask(model);
        }

        static void UpdateRumbleFromModel(RumbleContext ctx, RumbleTaskModel model) {
            if (model.motorType == MotorType.Left) {
                var entity = ctx.currentLeftRumble;
                entity.UpdateRumbleFromModel(model);
            } else if (model.motorType == MotorType.Right) {
                var entity = ctx.currentRightRumble;
                entity.UpdateRumbleFromModel(model);
            } else if (model.motorType == MotorType.Both) {
                var leftEntity = ctx.currentLeftRumble;
                leftEntity.UpdateRumbleFromModel(model);
                var rightEntity = ctx.currentRightRumble;
                rightEntity.UpdateRumbleFromModel(model);
            }
        }

        internal static void TickRumble(RumbleContext ctx, float dt, out float leftFreq, out float rightFreq) {

            // Apply Task
            ApplyTaskTime(ctx, dt);

            // Apply Rumble
            var leftRumble = ctx.currentLeftRumble;
            var rightRumble = ctx.currentRightRumble;
            ApplyRumble(leftRumble, dt);
            ApplyRumble(rightRumble, dt);

            leftFreq = leftRumble.currentFreq;
            rightFreq = rightRumble.currentFreq;

        }

        static void ApplyTaskTime(RumbleContext ctx, float dt) {
            var len = ctx.GetAllTask(out var modelArr);
            if (len == 0) {
                return;
            }
            for (var i = 0; i < len; i++) {
                var model = modelArr[i];
                model.delay -= dt;
                if (model.delay <= 0) {
                    UpdateRumbleFromModel(ctx, model);
                }
                ctx.UpdateTask(model, i);
            }
            ctx.RemoveAllReadyTask();
        }

        static void ApplyRumble(RumbleEntity rumble, float dt) {
            if (rumble.isFinished) {
                return;
            }
            rumble.currentTime += dt;
            if (rumble.currentTime >= rumble.duration) {
                rumble.currentFreq = 0;
                rumble.isFinished = true;
                return;
            }
            rumble.currentFreq = EasingHelper.Easing(rumble.startFreq, rumble.endFreq, rumble.currentTime, rumble.duration, rumble.easingType, rumble.easingMode);
        }

    }

}