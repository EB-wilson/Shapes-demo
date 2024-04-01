using UnityEngine;
using Motion = Shapes.Components.Motion;

namespace Shapes.Logic
{
    public class MotionSpeedControlTask: ScheduleTask
    {
        public float targetSpeed;
        public Motion motion;

        private float beginSpeed;

        protected override void begin()
        {
            beginSpeed = motion.speed;
        }

        protected override void action()
        {
            motion.speed = beginSpeed + (targetSpeed - beginSpeed)*progress;
        }

        protected override void post()
        {
        }
    }
}