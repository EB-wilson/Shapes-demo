using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shapes.Logic
{
    /// <summary>
    /// 时间表任务组，可同时批量执行多个任务，此任务运行直到所有任务被执行完毕
    /// </summary>
    public class TaskGroup : ScheduleTask
    {
        public List<ScheduleTask> tasks = new();
        public bool circle;

        public TaskGroup(params ScheduleTask[] tasks)
        {
            this.tasks.AddRange(tasks);
        }

        public override bool isComplete
        {
            get
            {
                return tasks.All(e => e.isComplete);
            }
        }

        public override void init(GameObject target)
        {
            initialized = true;

            reset();
            duration = 0;
            foreach (var task in tasks)
            {
                duration = Mathf.Max(duration, task.time + task.duration);
                task.init(target);
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
            time += timeDelta;

            foreach (var task in tasks.Where(task => task.beginTime <= time && !task.isComplete))
            {
                task.update(timeDelta);
            }

            if (isComplete && circle)
            {
                reset();
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

        public override ScheduleTask clone()
        {
            var res = new TaskGroup{ circle = circle };

            foreach (var scheduleTask in tasks)
            {
                res.tasks.Add(scheduleTask.clone());
            }

            return res;
        }
    }
}