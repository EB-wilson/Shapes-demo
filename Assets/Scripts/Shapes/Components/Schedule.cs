using System;
using System.Collections.Generic;
using System.Linq;
using Shapes.Logic;
using UnityEngine;

namespace Shapes.Components
{
    public class Schedule : MonoBehaviour
    {
        public bool pause;

        public List<Task> taskQueue = new();

        public float time;

        public int queueTasks => taskQueue.Count;

        private void Start()
        {
            foreach (var task in taskQueue)
            {
                task.init();
            }
        }

        private void Update()
        {
            updateTasks(Time.deltaTime);
        }

        public void updateTasks(float timeDelta)
        {
            if (pause) return;

            time += timeDelta;

            if (!taskQueue.Any()) return;

            foreach (var task in taskQueue.Where(task => !task.isComplete && task.beginTime <= time))
            {
                task.update(timeDelta);
            }

            taskQueue.RemoveAll(t => t.isComplete);
        }

        public void addTask(Task task)
        {
            taskQueue.Add(task);
            task.init();
        }

        public void postTopTask()
        {
            if (!taskQueue.Any()) return;

            var posted = taskQueue.First();
            posted.finalize();
            taskQueue.RemoveAt(0);
        }

        public void clearTasks(bool post)
        {
            if (post)
            {
                foreach (var task in taskQueue)
                {
                    task.finalize();
                }
            }

            taskQueue.Clear();
        }
    }
}