using Shapes.Components;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public abstract class ShootPattern: ICloneable<ShootPattern>
    {
        public int shoots;
        public float firstInterval;
        public float shootInterval;

        protected int shootCount;

        public abstract void shoot(Shooter shooter);

        public abstract ShootPattern clone();
    }
}