using Shapes.Components;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public abstract class ShootPattern: MonoBehaviour
    {
        public int shoots;
        public float shootInterval;

        protected int shootCount;

        public abstract void shoot(Shooter shooter);
    }
}