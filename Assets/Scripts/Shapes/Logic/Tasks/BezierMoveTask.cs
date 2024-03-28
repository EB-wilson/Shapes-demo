using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Logic
{
    public class BezierMoveTask: Task
    {
        public List<Vector3> controlPoints = new();

        private Vector3[] pathBuffer;

        private Vector3 beginPos;

        protected override void begin()
        {
            beginPos = transform.position;

            var slice = slicePoints(controlPoints.ToArray());
            controlPoints.Clear();
            controlPoints.Add(slice[0]);
            for (var i = 0; i < slice.Length; i++)
            {
                var bezierPart = bezierPoints(
                    slice[i],
                    slice[Mathf.Min(i + 1, slice.Length - 1)],
                    slice[Mathf.Min(i + 2, slice.Length - 1)],
                    slice[Mathf.Min(i + 3, slice.Length - 1)],
                    18
                );
                for (var j = 1; j < bezierPart.Length; j++)
                {
                    controlPoints.Add(slice[j]);
                }
            }
        }

        protected override void action()
        {
            var step = 1f/pathBuffer.Length;
            var i = (int)(progress*pathBuffer.Length);

            var t = (progress - i * step)/step;

            var s = pathBuffer[i];
            var n = pathBuffer[Mathf.Min(i + 1, pathBuffer.Length - 1)];

            transform.position = beginPos + (n - s) * t;
        }

        protected override void post()
        {

        }

        private static Vector3[] slicePoints(Vector3[] points)
        {
            var buffer = new Vector3[points.Length + points.Length/3 - 1];

            var index = 0;
            for (var i = 0; i < points.Length - 1; i++)
            {
                var s = points[i];

                buffer[index] = s;
                index++;
                if ((i + 1) % 3 != 0) continue;

                var n = points[i + 1];
                buffer[index] = (s + n) / 2;
                index++;
            }

            return buffer;
        }

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
    }
}