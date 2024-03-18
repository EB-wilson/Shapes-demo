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

        public Queue<Task> taskQueue = new();

        public int queueTasks => taskQueue.Count;

        private void Update()
        {
            updateTasks(Time.deltaTime);
        }

        public void updateTasks(float timeDelta)
        {
            if (pause) return;

            if (!taskQueue.Any()) return;
            var exec = taskQueue.Peek();

            exec.update(timeDelta);

            if (exec.isComplete)
            {
                taskQueue.Dequeue();
            }

            foreach (var task in taskQueue.Where(task => task != exec && task.async && !task.isComplete))
            {
                task.update(timeDelta);
            }
        }

        public void addTask(Task task)
        {
            taskQueue.Enqueue(task);
            task.init();
        }

        public void postTopTask()
        {
            if (!taskQueue.Any()) return;

            var posted = taskQueue.Dequeue();
            posted.finalize();
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