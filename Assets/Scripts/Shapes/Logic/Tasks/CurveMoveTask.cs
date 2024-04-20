using System;
using System.Collections.Generic;
using UnityEngine;
using Math = Shapes.Utils.Math;

namespace Shapes.Logic
{
    /// <summary>
    /// 贝塞尔曲线移动控制器，提供若干曲线控制点，随时间插值令携带该任务的物体沿着此贝塞尔曲线移动
    /// </summary>

    public class CurveMoveTask: ScheduleTask
    {
        public Vector3[] pathPoints;
        public bool syncForward;
        public bool smooth = true;

        private Vector3 beginPos;

        protected override void begin()
        {
            beginPos = self.transform.position;
            if (smooth)
            {
                pathPoints = Math.lengthSmooth(pathPoints, pathPoints.Length);
            }
        }

        protected override void action()
        {

            var prog = duration < 0? Mathf.Clamp01(time): progress;

            var step = 1f/pathPoints.Length;
            var i = (int)(prog*pathPoints.Length);

            var t = (prog - i * step)/step;

            var s = pathPoints[Mathf.Min(i, pathPoints.Length - 1)];
            var n = pathPoints[Mathf.Min(i + 1, pathPoints.Length - 1)];

            var trans = self.transform;
            trans.position = beginPos + s + (n - s) * t;
            if (syncForward)
            {
                trans.LookAt(trans.position + (n - s));
            }

        }

        protected override void post()
        {

        }

        public override ScheduleTask clone()
        {
            return new CurveMoveTask { duration = duration, beginTime = beginTime, interp = interp,
                pathPoints = pathPoints, syncForward = syncForward};
        }
    }
}