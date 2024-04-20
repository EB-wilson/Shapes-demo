using UnityEngine;
using Motion = Shapes.Components.Motion;

namespace Shapes.Logic
{
    public class SpeedRotateTask: ScheduleTask
    {
        public Vector3 moveRot;

        private Motion motion;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void begin()
        {
            motion = self.GetComponent<Motion>();
        }

        protected override void action()
        {
            motion.vel = Quaternion.Euler(moveRot*Time.deltaTime)*motion.vel;
        }

        protected override void post() { }

        public override ScheduleTask clone()
        {
            return new SpeedRotateTask { duration = duration, beginTime = beginTime, interp = interp,
                moveRot = moveRot };
        }
    }
}