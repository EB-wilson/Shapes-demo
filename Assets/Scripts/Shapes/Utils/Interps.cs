using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shapes.Utils
{
    public enum Interp
    {
        LINEAR,
        INV_LINEAR,
        HALF_LINEAR,
        HALF_CLAMP,
        POW2,
        POW3,
        POW4,
        POW2_OUT,
        POW3_OUT,
        POW4_OUT
    }

    public static class Interps
    {
        private static Dictionary<Interp, Func<float, float>> interps = new();

        static Interps()
        {
            interps[Interp.LINEAR] = f => f;
            interps[Interp.INV_LINEAR] = f => 1 - f;
            interps[Interp.HALF_LINEAR] = f => 1 - Mathf.Abs(1 - f*2);
            interps[Interp.HALF_CLAMP] = clamp(0.5f, 1f);
            interps[Interp.POW2] = f => f*f;
            interps[Interp.POW3] = f => f*f*f;
            interps[Interp.POW4] = f => f*f*f*f;
            interps[Interp.POW2_OUT] = powOut(2);
            interps[Interp.POW3_OUT] = powOut(3);
            interps[Interp.POW4_OUT] = powOut(4);
        }

        private static Func<float, float> clamp(float min, float max) => f => Mathf.Clamp01(min + Mathf.Max(f - min, 0)/(max - min));
        private static Func<float, float> powOut(float power) => f => MathF.Pow(f - 1, power) + 1;

        public static Func<float, float> wrap(Func<float, float> fun, Func<float, float> wrap) => f => wrap(fun(f));

        public static Func<float, float> make(Interp[] interpList)
        {
            Func<float, float> res = null;

            foreach (var interp in interpList)
            {
                var l = interps[interp];
                res = res != null ? wrap(res, l) : l;
            }

            return res;
        }
    }
}