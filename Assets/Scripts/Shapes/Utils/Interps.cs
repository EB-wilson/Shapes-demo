using System;
using UnityEngine;

namespace Shapes.Utils
{
    public static class Interps
    {
        //linear
        public static readonly Func<float, float> LINEAR = f => f;
        public static readonly Func<float, float> INV_LINEAR = f => 1 - f;
        public static readonly Func<float, float> HALF_LINEAR = f => 1 - Mathf.Abs(1 - f*2);

        //clamp
        public static Func<float, float> clamp(float min, float max) => f => Mathf.Clamp01(min + Mathf.Max(f - min, 0)/(max - min));
        public static readonly Func<float, float> HALF_CLAMP = clamp(0.5f, 1f);

        //pow
        public static readonly Func<float, float> POW2 = f => f * f;
        public static readonly Func<float, float> POW3 = f => f * f * f;
        public static readonly Func<float, float> POW4 = f => f * f * f * f;

        //pow out
        public static Func<float, float> pow(float power) => f => MathF.Pow(f - 1, power) + 1;

        public static readonly Func<float, float> POW2_OUT = pow(2);
        public static readonly Func<float, float> POW3_OUT = pow(3);
        public static readonly Func<float, float> POW4_OUT = pow(4);

        public static Func<float, float> wrap(Func<float, float> fun, Func<float, float> wrap) => f => wrap(fun(f));
    }
}