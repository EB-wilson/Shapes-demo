using Shapes.Components;
using UnityEngine;

namespace Shapes.Logic
{
    public class DestroyTask: ScheduleTask
    {
        public bool kill;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void begin()
        {
            if (kill)
            {
                var hel = self.GetComponent<Health>();
                if (hel != null)
                {
                    hel.kill();
                    return;
                }
            }
            Object.Destroy(self);
        }

        protected override void action() { }

        protected override void post() { }

        public override ScheduleTask clone()
        {
            return new DestroyTask{ duration = duration, beginTime = beginTime, interp = interp, kill = kill };
        }
    }
}