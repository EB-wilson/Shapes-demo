using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Logic
{
    /// <summary>
    /// 贝塞尔曲线移动控制器，提供若干曲线控制点，随时间插值令携带该任务的物体沿着此贝塞尔曲线移动
    /// </summary>

    public class BezierMoveTask: ScheduleTask
    {
        public List<Vector3> controlPoints = new();
        public int resolution = 100;
        public bool syncForward;

        private Vector3[] pathBuffer;

        private Vector3 beginPos;

        protected override void begin()
        {
            beginPos = transform.position;

            var slice = slicePoints(controlPoints.ToArray());
            var buffPoints = new List<Vector3>();
            buffPoints.Clear();

            // 按曲线控制点分割开的控制序列依次生成并附加路径序列到缓冲
            for (var i = 0; i < slice.Length; i += 3)
            {
                var bezierPart = bezierPoints(
                    slice[i],
                    slice[Mathf.Min(i + 1, slice.Length - 1)],
                    slice[Mathf.Min(i + 2, slice.Length - 1)],
                    slice[Mathf.Min(i + 3, slice.Length - 1)],
                    resolution/(slice.Length/3 + 1)*2
                );
                buffPoints.AddRange(bezierPart);
            }

            // 最终步长平滑化
            pathBuffer = lengthSmooth(buffPoints.ToArray(), resolution);
        }

        protected override void action()
        {

            var prog = duration < 0? Mathf.Clamp01(time): progress;

            var step = 1f/pathBuffer.Length;
            var i = (int)(prog*pathBuffer.Length);

            var t = (prog - i * step)/step;

            var s = pathBuffer[Mathf.Min(i, pathBuffer.Length - 1)];
            var n = pathBuffer[Mathf.Min(i + 1, pathBuffer.Length - 1)];

            var trans = transform;
            trans.position = beginPos + s + (n - s) * t;
            if (syncForward)
            {
                trans.LookAt(trans.position + (n - s));
            }

        }

        protected override void post()
        {

        }

        /// <summary>
        /// 将控制点序列切分为符合三阶贝塞尔曲线衔接的插入若干中间点的控制点序列
        /// </summary>
        /// <param name="points">原始控制点序列</param>
        /// <returns>插入分割点的控制点序列</returns>
        private static Vector3[] slicePoints(IReadOnlyList<Vector3> points)
        {
            var buffer = new Vector3[points.Count + (points.Count - 1)/3];

            var index = 0;
            for (var i = 0; i < points.Count; i++)
            {
                var s = points[i];

                buffer[index] = s;
                index++;
                if ((i + 1) % 3 != 0 || i + 1 >= points.Count) continue;

                var n = points[i + 1];
                buffer[index] = (s + n) / 2;
                index++;
            }

            return buffer;
        }

        /// <summary>
        /// 三阶贝塞尔曲线生成函数
        /// </summary>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">控制点3</param>
        /// <param name="p4">控制点4</param>
        /// <param name="resolution">曲线的分辨率，或者叫做段数，即插值从0-1均分为夺少个路径点</param>
        /// <returns>贝塞尔曲线路径序列</returns>
        private static Vector3[] bezierPoints(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int resolution)
        {
            var res = new Vector3[resolution];
            var tStep = 1.0f / resolution;

            for (var i = 0; i < resolution; i++)
            {
                var t = tStep * i;
                res[i] = Mathf.Pow(1 - t, 3)*p1 + 3*t*Mathf.Pow(1 - t, 2)*p2 + 3*Mathf.Pow(t, 2)*(1 - t)*p3 + Mathf.Pow(t, 3)*p4;
            }

            return res;
        }

        /// <summary>
        /// 路径步进平滑函数，使路径的实际点数据点间隔与给定的分辨率相匹配。
        /// </summary>
        /// <param name="path">路径数据序列（它们的位移间隔可能完全不一致）</param>
        /// <param name="resolution">路径的目标分辨率，路径将会重新平均的分布到原路径上</param>
        /// <returns>间隔平滑后的路径序列</returns>
        private static Vector3[] lengthSmooth(Vector3[] path, int resolution)
        {
            var res = new Vector3[resolution];

            var totalLen = 0f;
            for (var i = 0; i < path.Length - 1; i++)
            {
                totalLen += (path[i + 1] - path[i]).magnitude;
            }

            var step = totalLen / resolution;
            var index = -1;
            var buffLen = 0f;

            var lastLen = 0f;
            Vector3 a, b;
            for (var i = 0; i < resolution; i++)
            {
                a = index == -1? path[index + 1]: path[index];
                b = path[index + 1];
                var diff = (b - a).magnitude;

                while (buffLen < step * i)
                {
                    lastLen = buffLen;
                    index++;
                    a = path[index];
                    b = path[index + 1];
                    diff = (b - a).magnitude;
                    buffLen += diff;
                }

                res[i] = diff == 0? a: a + (b - a) * (step*i - lastLen) / diff;
            }

            return res;
        }
    }
}