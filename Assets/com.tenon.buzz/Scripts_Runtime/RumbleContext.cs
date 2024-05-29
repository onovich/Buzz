using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Buzz.Sample {

    public class RumbleContext {

        public RumbleEntity currentLeftRumble;
        public RumbleEntity currentRightRumble;

        public List<RumbleTaskModel> all;
        public Queue<RumbleTaskModel> allTasksQueue;

        public RumbleTaskModel[] readyTemp;
        public RumbleTaskModel[] temp;

        public RumbleContext() {
            all = new List<RumbleTaskModel>();
            allTasksQueue = new Queue<RumbleTaskModel>();
            currentLeftRumble = new RumbleEntity();
            currentRightRumble = new RumbleEntity();
            readyTemp = new RumbleTaskModel[20];
            temp = new RumbleTaskModel[20];
        }

        public void SetLeftRumble(RumbleEntity entity) {
            currentLeftRumble = entity;
        }

        public void SetRightRumble(RumbleEntity entity) {
            currentRightRumble = entity;
        }

        public void AddTask(RumbleTaskModel model) {
            allTasksQueue.Enqueue(model);
        }

        public int GetAllTask(out RumbleTaskModel[] modelArray) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new RumbleTaskModel[(int)(count * 1.5f)];
            }
            all.CopyTo(temp, 0);
            modelArray = temp;
            return count;
        }

        public int GetAllReadyTask(out RumbleTaskModel[] modelArray) {
            int count = 0;
            for (int i = 0; i < all.Count; i++) {
                var model = all[i];

                if (count >= readyTemp.Length) {
                    var newReadyTemp = new RumbleTaskModel[(int)(count * 1.5f)];
                    readyTemp.CopyTo(newReadyTemp, 0);
                    readyTemp = newReadyTemp;
                }

                if (model.delay <= 0) {
                    readyTemp[i] = model;
                    count++;
                }
            }

            modelArray = readyTemp;
            return count;
        }

        public void Clear() {
            allTasksQueue.Clear();
        }

    }

}