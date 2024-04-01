using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Utils
{
    public static class Times
    {
        private static List<TimeTask> timeTasks = new();

        public static void updateTask()
        {
            foreach (var task in timeTasks.Where(task => task.beginTime + task.delay <= Time.time))
            {
                task.action();
            }

            timeTasks.RemoveAll(task => task.beginTime + task.delay <= Time.time);
        }

        class TimeTask
        {
            public Action action;
            public float delay;
            public float beginTime;

            public TimeTask(Action action, float delay)
            {
                this.action = action;
                this.delay = delay;
                beginTime = Time.time;
            }
        }

        public static void run(Action action, float delay)
        {
            timeTasks.Add(new TimeTask(action, delay));
        }

        public static void runTask(Action action, float delay)
        {
            new Thread(() =>
            {
                Thread.Sleep((int)(delay*1000));
                action();
            }).Start();
        }
    }
}