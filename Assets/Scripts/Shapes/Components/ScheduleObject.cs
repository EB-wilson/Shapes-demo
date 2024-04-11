using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Shapes.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Components
{
    public class ScheduleObject : MonoBehaviour
    {
        public bool pause;
        public List<ScheduleTask> taskList = new();
        public bool destroyOnEnd;

        public float time;

        public int queueTasks => taskList.Count;

        private void Start()
        {
            foreach (var task in taskList)
            {
                task.init(gameObject);
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

            if (!taskList.Any()) return;

            foreach (var task in taskList.Where(task => !task.isComplete && task.beginTime <= time))
            {
                task.update(timeDelta);
            }

            taskList.RemoveAll(t => t.isComplete);

            if (destroyOnEnd && taskList.Count == 0)
            {
                Destroy(gameObject);
            }
        }

        public void addTask(ScheduleTask task)
        {
            taskList.Add(task);
            task.init(gameObject);
        }

        public void addTasks(params ScheduleTask[] tasks)
        {
            taskList.AddRange(tasks);
            foreach (var task in tasks)
            {
                task.init(gameObject);
            }
        }

        public void postTopTask()
        {
            if (!taskList.Any()) return;

            var posted = taskList.First();
            posted.finalize();
            taskList.RemoveAt(0);
        }

        public void clearTasks(bool post)
        {
            if (post)
            {
                foreach (var task in taskList)
                {
                    task.finalize();
                }
            }

            taskList.Clear();
        }

        public ScheduleObject makeInst()
        {
            var trans = transform;
            return makeInst(trans.position, trans.rotation);
        }

        public ScheduleObject makeInst(Vector3 pos, Quaternion rot)
        {
            var res = Instantiate(gameObject, pos, rot).GetComponent<ScheduleObject>();
            foreach (var task in taskList)
            {
                res.addTask(task.clone());
            }

            return res;
        }
    }
}