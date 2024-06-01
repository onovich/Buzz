using MortiseFrame.Swing;
using UnityEngine.UIElements.Experimental;

namespace TenonKit.Buzz.Sample {

    internal static class RumbleDomain {

        internal static void CreateRumbleTaskModel(RumbleContext ctx, MotorType motorType, float delay, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            var model = RumbleFactory.CreateRumbleTaskModel(motorType, delay, startFreq, endFreq, duration, easingType, easingMode);
            ctx.AddTask(model);
        }

        static void CreateNewRumbleEntityFromModel(RumbleContext ctx, RumbleTaskModel model) {
            var entity = RumbleFactory.CreateRumbleEntity(model);
            if (model.motorType == MotorType.Left) {
                ctx.SetLeftRumble(entity);
            } else {
                ctx.SetRightRumble(entity);
            }
        }

        static void UpdateRumbleFromModel(RumbleContext ctx, RumbleTaskModel model) {
            if (model.motorType == MotorType.Left) {
                var entity = ctx.currentLeftRumble;
                RumbleFactory.UpdateRumbleFromModel(entity, model);
                return;
            }
            if (model.motorType == MotorType.Right) {
                var entity = ctx.currentRightRumble;
                RumbleFactory.UpdateRumbleFromModel(entity, model);
                return;
            }
            if (model.motorType == MotorType.Both) {
                var leftEntity = ctx.currentLeftRumble;
                RumbleFactory.UpdateRumbleFromModel(leftEntity, model);
                var rightEntity = ctx.currentRightRumble;
                RumbleFactory.UpdateRumbleFromModel(rightEntity, model);
            }
        }

        internal static void TickRumble(RumbleContext ctx, float dt) {

            // Apply Task
            ApplyTaskTime(ctx, dt);
            ApplyCheckTask(ctx);

            // Apply Rumble
            if (ctx.currentLeftRumble.isFinished == false) {
                ApplyRumble(ctx.currentLeftRumble, dt);
            }
            if (ctx.currentRightRumble.isFinished == false) {
                ApplyRumble(ctx.currentRightRumble, dt);
            }
        }

        static void ApplyTaskTime(RumbleContext ctx, float dt) {
            var len = ctx.GetAllTask(out var modelArr);
            if (len == 0) {
                return;
            }
            for (var i = 0; i < len; i++) {
                var model = modelArr[i];
                model.delay -= dt;
            }
        }

        static void ApplyCheckTask(RumbleContext ctx) {
            var len = ctx.GetAllReadyTask(out var modelArr);
            if (len == 0) {
                return;
            }
            for (var i = 0; i < len; i++) {
                var model = modelArr[i];

                if (model.motorType == MotorType.Left) {
                    if (!IsLeftMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                        return;
                    }
                    UpdateRumbleFromModel(ctx, model);
                    return;
                }

                if (model.motorType == MotorType.Right) {
                    if (!IsRightMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                        return;
                    }
                    UpdateRumbleFromModel(ctx, model);
                    return;
                }

                if (model.motorType == MotorType.Both) {
                    if (!IsLeftMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                    } else {
                        UpdateRumbleFromModel(ctx, model);
                    }
                    if (!IsRightMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                    } else {
                        UpdateRumbleFromModel(ctx, model);
                    }
                    return;
                }

            }
        }

        static void ApplyRumble(RumbleEntity rumble, float dt) {
            if (rumble.isFinished) {
                return;
            }
            rumble.currentTime += dt;
            if (rumble.currentTime >= rumble.duration) {
                rumble.isFinished = true;
                return;
            }
            rumble.currentFreq = EasingHelper.Easing(rumble.startFreq, rumble.endFreq, rumble.currentTime, rumble.duration, rumble.easingType, rumble.easingMode);
        }

        static bool IsLeftMotorRubbling(RumbleContext ctx) {
            return ctx.currentLeftRumble.isFinished == false;
        }

        static bool IsRightMotorRubbling(RumbleContext ctx) {
            return ctx.currentRightRumble.isFinished == false;
        }

    }

}