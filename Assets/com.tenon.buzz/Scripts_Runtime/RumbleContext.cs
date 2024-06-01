using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Buzz {

    internal class RumbleContext {

        internal RumbleEntity currentLeftRumble;
        internal RumbleEntity currentRightRumble;

        internal List<RumbleTaskModel> all;

        internal RumbleTaskModel[] readyTemp;
        internal RumbleTaskModel[] temp;

        internal RumbleContext() {
            all = new List<RumbleTaskModel>();
            currentLeftRumble = new RumbleEntity();
            currentRightRumble = new RumbleEntity();
            readyTemp = new RumbleTaskModel[20];
            temp = new RumbleTaskModel[20];
        }

        internal void SetLeftRumble(RumbleEntity entity) {
            currentLeftRumble = entity;
        }

        internal void SetRightRumble(RumbleEntity entity) {
            currentRightRumble = entity;
        }

        internal void AddTask(RumbleTaskModel model) {
            all.Add(model);
        }

        internal int GetAllTask(out RumbleTaskModel[] modelArray) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new RumbleTaskModel[(int)(count * 1.5f)];
            }
            all.CopyTo(temp, 0);
            modelArray = temp;
            return count;
        }

        internal void UpdateTask(RumbleTaskModel model, int index) {
            all[index] = model;
        }

        internal int TakeAllReadyTask(out RumbleTaskModel[] modelArray) {
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

            for (int i = 0; i < count; i++) {
                all.Remove(readyTemp[i]);
            }

            return count;
        }

        internal void Clear() {
            all.Clear();
        }

    }

}