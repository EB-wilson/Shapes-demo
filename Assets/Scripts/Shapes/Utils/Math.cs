using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Utils
{
    public class Math
    {
        public static readonly int[] single = { -1, 1 };

        public static float angle(float x, float y)
        {
            if (x == 0) return y > 0? 90: 270;

            float absAng = Mathf.Atan(y / x)*Mathf.Rad2Deg;

            if (x > 0) return absAng;
            return absAng + 180;
        }

        public static float approach(float from, float to, float speed)
        {
            var diff = to - from;
            var d = diff > 0 ? 1 : -1;
            var b = speed > 0 ? 1 : -1;

            return from + Mathf.Min(Mathf.Abs(diff), Mathf.Abs(speed))*d*b;
        }

        public static Vector3[] bezireCurve(Vector3[] ctrlPoints, int resolution = 100)
        {
            var buffer = new Vector3[ctrlPoints.Length + (ctrlPoints.Length - 1)/3];

            var index = 0;
            for (var i = 0; i < ctrlPoints.Length; i++)
            {
                var s = ctrlPoints[i];

                buffer[index] = s;
                index++;
                if ((i + 1) % 3 != 0 || i + 1 >= ctrlPoints.Length) continue;

                var n = ctrlPoints[i + 1];
                buffer[index] = (s + n) / 2;
                index++;
            }

            var buffPoints = new List<Vector3>();
            buffPoints.Clear();

            // 按曲线控制点分割开的控制序列依次生成并附加路径序列到缓冲
            for (var i = 0; i < buffer.Length; i += 3)
            {
                var bezierPart = bezierPoints(
                    buffer[i],
                    buffer[Mathf.Min(i + 1, buffer.Length - 1)],
                    buffer[Mathf.Min(i + 2, buffer.Length - 1)],
                    buffer[Mathf.Min(i + 3, buffer.Length - 1)],
                    resolution/(buffer.Length/3 + 1)*2
                );
                buffPoints.AddRange(bezierPart);
            }

            return buffPoints.ToArray();
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
        public static Vector3[] lengthSmooth(Vector3[] path, int resolution)
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