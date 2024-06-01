using MortiseFrame.Swing;
using UnityEngine.UIElements.Experimental;

namespace TenonKit.Buzz {

    internal static class RumbleDomain {

        internal static void CreateRumbleTaskModel(RumbleContext ctx, MotorType motorType, float delay, float startFreq, float endFreq, float duration, EasingType easingType, EasingMode easingMode) {
            var model = RumbleFactory.CreateRumbleTaskModel(motorType, delay, startFreq, endFreq, duration, easingType, easingMode);
            ctx.AddTask(model);
        }

        static void CreateNewRumbleEntityFromModel(RumbleContext ctx, RumbleTaskModel model) {
            var entity = RumbleFactory.CreateRumbleEntity(model);
            if (model.motorType == MotorType.Left) {
                ctx.SetLeftRumble(entity);
            } else if (model.motorType == MotorType.Left) {
                ctx.SetRightRumble(entity);
            } else {
                BLog.Error("RumbleCore " + "CreateNewRumbleEntityFromModel " + "motorType is not valid");
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

        internal static void TickRumble(RumbleContext ctx, float dt, out float leftFreq, out float rightFreq) {

            // Apply Task
            ApplyTaskTime(ctx, dt);
            ApplyCheckTask(ctx);

            // Apply Rumble
            if (ctx.currentLeftRumble != null && ctx.currentLeftRumble.isFinished == false) {
                ApplyRumble(ctx.currentLeftRumble, dt);
                // BLog.Log("RumbleCore " + "TickRumble " + "leftFreq: " + ctx.currentLeftRumble.currentFreq);
            }
            if (ctx.currentRightRumble != null && ctx.currentRightRumble.isFinished == false) {
                ApplyRumble(ctx.currentRightRumble, dt);
                // BLog.Log("RumbleCore " + "TickRumble " + "rightFreq: " + ctx.currentRightRumble.currentFreq);
            }

            var leftRumble = ctx.currentLeftRumble;
            var rightRumble = ctx.currentRightRumble;

            leftFreq = leftRumble.isFinished ? 0 : leftRumble.currentFreq;
            rightFreq = rightRumble.isFinished ? 0 : rightRumble.currentFreq;
        }

        static void ApplyTaskTime(RumbleContext ctx, float dt) {
            var len = ctx.GetAllTask(out var modelArr);
            if (len == 0) {
                return;
            }
            for (var i = 0; i < len; i++) {
                var model = modelArr[i];
                model.delay -= dt;
                ctx.UpdateTask(model, i);
            }
        }

        static void ApplyCheckTask(RumbleContext ctx) {
            var len = ctx.TakeAllReadyTask(out var modelArr);
            if (len == 0) {
                return;
            }

            for (var i = 0; i < len; i++) {
                var model = modelArr[i];

                if (model.motorType == MotorType.Left) {
                    if (!HasLeftMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                        continue;
                    }
                    UpdateRumbleFromModel(ctx, model);
                    continue;
                }

                if (model.motorType == MotorType.Right) {
                    if (!HasRightMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                        continue;
                    }
                    UpdateRumbleFromModel(ctx, model);
                    continue;
                }

                if (model.motorType == MotorType.Both) {
                    if (!HasLeftMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                    } else {
                        UpdateRumbleFromModel(ctx, model);
                    }
                    if (!HasRightMotorRubbling(ctx)) {
                        CreateNewRumbleEntityFromModel(ctx, model);
                    } else {
                        UpdateRumbleFromModel(ctx, model);
                    }
                    continue;
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

        static bool HasLeftMotorRubbling(RumbleContext ctx) {
            return ctx.currentLeftRumble != null;
        }

        static bool HasRightMotorRubbling(RumbleContext ctx) {
            return ctx.currentRightRumble != null;
        }

        static bool IsLeftMotorRubbling(RumbleContext ctx) {
            return ctx.currentLeftRumble.isFinished == false;
        }

        static bool IsRightMotorRubbling(RumbleContext ctx) {
            return ctx.currentRightRumble.isFinished == false;
        }

    }

}