using UnityEngine;

namespace Shapes.Logic
{
    /// <summary>
    /// 任务序列，按照给出的任务清单依次执行，直至所有任务运行完毕
    /// </summary>
    public class TaskSequence : TaskGroup
    {
        private int executing;

        public override bool isComplete => executing >= tasks.Count;

        public override void init(GameObject target)
        {
            initialized = true;

            reset();
            duration = 0;
            foreach (var task in tasks)
            {
                duration += task.duration;
                task.init(target);
            }
        }

        public override void update(float timeDelta)
        {
            if (isComplete)
            {
                if (circle)
                {
                    reset();
                }
                return;
            }

            var task = tasks[executing];

            if (!task.paused)
            {
                time += timeDelta;
            }

            task.update(timeDelta);
            if (task.isComplete)
            {
                executing++;
            }
        }

        public override void finalize()
        {
            base.finalize();
            executing = tasks.Count;
        }

        public override void reset()
        {
            base.reset();
            executing = 0;
        }
    }
}