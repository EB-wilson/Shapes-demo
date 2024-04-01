using UnityEngine;
using Motion = Shapes.Components.Motion;

namespace Shapes.Logic
{
    public class MotionControlTask: ScheduleTask
    {
        public Vector3 targetMotion;
        public Motion motion;

        private Vector3 beginMotion;

        private void Start()
        {
            if (motion == null)
            {
                motion = GetComponent<Motion>();
            }
        }

        protected override void begin()
        {
            beginMotion = motion.vel;
        }

        protected override void action()
        {
            motion.vel = Vector3.Lerp(beginMotion, targetMotion, progress);
        }

        protected override void post()
        {
            throw new System.NotImplementedException();
        }
    }
}