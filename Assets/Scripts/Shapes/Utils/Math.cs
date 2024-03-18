using UnityEngine;

namespace Shapes.Utils
{
    public class Math
    {
        public static float angle(float x, float y)
        {
            if (x == 0) return y > 0? 90: 270;

            float absAng = Mathf.Atan(y / x)*Mathf.Rad2Deg;

            if (x > 0) return absAng;
            else return absAng + 180;
        }
    }
}