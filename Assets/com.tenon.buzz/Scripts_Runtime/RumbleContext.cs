using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Buzz {

    internal class RumbleContext {

        internal RumbleEntity currentLeftRumble;
        internal RumbleEntity currentRightRumble;

        internal List<RumbleTaskModel> allTask;

        internal RumbleTaskModel[] readyTaskTemp;
        internal RumbleTaskModel[] allTasktemp;

        internal RumbleContext() {
            allTask = new List<RumbleTaskModel>();
            currentLeftRumble = new RumbleEntity();
            currentRightRumble = new RumbleEntity();
            readyTaskTemp = new RumbleTaskModel[20];
            allTasktemp = new RumbleTaskModel[20];
        }

        internal void SetLeftRumble(RumbleEntity entity) {
            currentLeftRumble = entity;
        }

        internal void SetRightRumble(RumbleEntity entity) {
            currentRightRumble = entity;
        }

        internal void AddTask(RumbleTaskModel model) {
            allTask.Add(model);
        }

        internal int GetAllTask(out RumbleTaskModel[] modelArray) {
            int count = allTask.Count;
            if (count > allTasktemp.Length) {
                allTasktemp = new RumbleTaskModel[(int)(count * 1.5f)];
            }
            allTask.CopyTo(allTasktemp, 0);
            modelArray = allTasktemp;
            return count;
        }

        internal void UpdateTask(RumbleTaskModel model, int index) {
            allTask[index] = model;
        }

        internal int TakeAllReadyTask(out RumbleTaskModel[] modelArray) {

            if (allTask.Count >= readyTaskTemp.Length) {
                readyTaskTemp = new RumbleTaskModel[(int)(allTask.Count * 1.5f)];
            }

            int count = 0;
            for (int i = 0; i < allTask.Count; i++) {
                var model = allTask[i];
                if (model.delay <= 0) {
                    readyTaskTemp[i] = model;
                    count++;
                }
            }

            modelArray = readyTaskTemp;

            if (count == 2) {
                Debug.Log("count==2: type: " + readyTaskTemp[0].motorType + " " + readyTaskTemp[1].motorType);
            }
            if (count == 1) {
                Debug.Log("count==1: type: " + readyTaskTemp[0].motorType);
            }
            return count;
        }

        internal void RemoveAllReadyTask() {
            allTask.RemoveAll(model => model.delay <= 0);
        }

        internal void Clear() {
            allTask.Clear();
        }

    }

}