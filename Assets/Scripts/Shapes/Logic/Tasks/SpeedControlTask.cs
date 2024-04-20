using UnityEngine;
using Motion = Shapes.Components.Motion;

namespace Shapes.Logic
{
    public class SpeedControlTask: ScheduleTask
    {
        public float beginSpeed = -1;
        public float toSpeed;

        private Motion motion;
        private bool speedUnset;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void begin()
        {
            motion = self.GetComponent<Motion>();
            speedUnset = beginSpeed < 0;
            if (speedUnset)
            {
                beginSpeed = motion.speed;
            }
        }

        protected override void action()
        {
            motion.speed = Mathf.Lerp(beginSpeed, toSpeed, progress);
        }

        protected override void post()
        {
            if (speedUnset)
            {
                beginSpeed = -1;
            }
        }

        public override ScheduleTask clone()
        {
            return new SpeedControlTask { duration = duration, beginTime = beginTime, interp = interp,
                beginSpeed = beginSpeed, toSpeed = toSpeed };
        }
    }
}