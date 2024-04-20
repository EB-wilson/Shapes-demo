using UnityEngine;
using Motion = Shapes.Components.Motion;

namespace Shapes.Logic
{
    public class RotSpeedControlTask: ScheduleTask
    {
        public float beginRotSpeed = -1;
        public float toRotSpeed;

        private Motion motion;
        private bool rotUnset;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void begin()
        {
            motion = self.GetComponent<Motion>();
            rotUnset = beginRotSpeed < 0;

            if (rotUnset)
            {
                beginRotSpeed = motion.rotSpeed;
            }
        }

        protected override void action()
        {
            motion.rotSpeed = Mathf.Lerp(beginRotSpeed, toRotSpeed, progress);
        }

        protected override void post()
        {
            if (rotUnset)
            {
                beginRotSpeed = -1;
            }
        }

        public override ScheduleTask clone()
        {
            return new RotSpeedControlTask { duration = duration, beginTime = beginTime, interp = interp,
                beginRotSpeed = beginRotSpeed, toRotSpeed = toRotSpeed };
        }
    }
}