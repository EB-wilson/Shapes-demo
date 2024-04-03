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
    }
}