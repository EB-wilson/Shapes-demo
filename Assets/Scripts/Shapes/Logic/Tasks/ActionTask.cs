using System;
using UnityEngine;

namespace Shapes.Logic
{
    public class ActionTask<T>: ScheduleTask where T: MonoBehaviour
    {
        public Action<T> beginAct;
        public Action<T, float> act;
        public Action<T> postAct;

        private T tmp;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void begin()
        {
            tmp = self.GetComponent<T>();
            beginAct?.Invoke(tmp);
        }

        protected override void action()
        {
            act?.Invoke(tmp, progress);
        }

        protected override void post()
        {
            postAct?.Invoke(tmp);
        }

        public override ScheduleTask clone()
        {
            return new ActionTask<T> { duration = duration, beginTime = beginTime, interp = interp,
                beginAct = beginAct, act = act, postAct = postAct };
        }
    }
}