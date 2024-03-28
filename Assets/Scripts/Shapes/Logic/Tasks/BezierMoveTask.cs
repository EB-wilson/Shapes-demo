using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Logic
{
    public class BezierMoveTask: Task
    {
        public List<Vector3> controlPoints = new();

        private Vector3[] pathBuffer;

        protected override void begin()
        {
            Vector3[] slice = slicePoints(controlPoints.ToArray());
            controlPoints.Clear();
            controlPoints.Add(slice[0]);
            for (int i = 0; i < slice.Length; i++)
            {
                Vector3[] bezierPart = bezierPoints(
                    slice[i],
                    slice[Mathf.Min(i + 1, slice.Length - 1)],
                    slice[Mathf.Min(i + 2, slice.Length - 1)],
                    slice[Mathf.Min(i + 3, slice.Length - 1)],
                    18
                );
                for (int j = 1; j < bezierPart.Length; j++)
                {
                    controlPoints.Add(slice[j]);
                }
            }
        }

        protected override void action()
        {

        }

        protected override void post()
        {

        }

        private Vector3[] slicePoints(Vector3[] points)
        {
            Vector3[] buffer = new Vector3[points.Length + points.Length/3 - 1];

            int index = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 s = points[i];

                buffer[index] = s;
                index++;
                if ((i + 1) % 3 != 0) continue;

                Vector3 n = points[i + 1];
                buffer[index] = (s + n) / 2;
                index++;
            }

            return buffer;
        }

        private Vector3[] bezierPoints(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int resolution)
        {
            Vector3[] res = new Vector3[resolution];
            float tStep = 1.0f / resolution;

            for (int i = 0; i < resolution; i++)
            {
                float t = tStep * i;
                res[i] = Mathf.Pow(1 - t, 3)*p1 + 3*t*Mathf.Pow(1 - t, 2)*p2 + 3*Mathf.Pow(t, 2)*(1 - t)*p3 + Mathf.Pow(t, 3)*p4;
            }

            return res;
        }
    }
}