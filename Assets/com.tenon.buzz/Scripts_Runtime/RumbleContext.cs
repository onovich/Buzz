using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Buzz.Sample {

    public class RumbleContext {

        public RumbleEntity currentLeftRumble;
        public RumbleEntity currentRightRumble;
        public Queue<RumbleTaskModel> allTasks;
        public RumbleTaskModel[] temp;
        public float currentTime;

        public RumbleContext() {
            allTasks = new Queue<RumbleTaskModel>();
            currentLeftRumble = new RumbleEntity();
            currentRightRumble = new RumbleEntity();
            currentTime = 0;
            temp = new RumbleTaskModel[4];
        }

        public void SetLeftRumble(RumbleEntity entity) {
            currentLeftRumble = entity;
        }

        public void SetRightRumble(RumbleEntity entity) {
            currentRightRumble = entity;
        }

        public void AddTask(RumbleTaskModel model) {
            allTasks.Enqueue(model);
        }

        public int TryGetReadyTask(out RumbleTaskModel[] modelArray) {
            var count = 0;
            while (allTasks.Count > count) {
                var model = allTasks.Peek();
                if (model.timeStamp <= currentTime) {
                    temp[count] = model;
                    allTasks.Dequeue();
                    count++;
                } else {
                    break;
                }
            }
            modelArray = temp;
            return count;
        }

        public void Clear() {
            allTasks.Clear();
        }

    }

}