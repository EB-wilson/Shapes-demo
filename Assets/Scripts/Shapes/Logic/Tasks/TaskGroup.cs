using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Logic
{
    /// <summary>
    /// 时间表任务组，可同时批量执行多个任务，此任务运行直到所有任务被执行完毕
    /// </summary>
    public class TaskGroup : Task
    {
        public readonly List<Task> tasks = new();

        public TaskGroup(params Task[] tasks)
        {
            this.tasks.AddRange(tasks);
        }

        public override bool isComplete
        {
            get
            {
                return tasks.Find(e => !e.isComplete) == null;
            }
        }

        public override void init()
        {
            initialized = true;

            reset();
            duration = 0;
            foreach (var task in tasks)
            {
                duration = Mathf.Max(duration, task.duration);
                task.init();
            }
        }

        public override void finalize()
        {
            time = duration;
            foreach (var task in tasks)
            {
                task.finalize();
            }
        }

        public override void reset()
        {
            base.reset();
            foreach (var task in tasks)
            {
                task.reset();
            }
        }

        public override void update(float timeDelta)
        {
            foreach (var task in tasks)
            {
                task.update(timeDelta);

                time = Mathf.Max(task.time, time);
            }
        }

        protected override void begin()
        {
            //no action
        }

        protected override void action()
        {
            //no action
        }

        protected override void post()
        {
            //no action
        }
    }
}