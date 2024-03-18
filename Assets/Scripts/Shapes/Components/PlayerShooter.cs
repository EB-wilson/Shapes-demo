using System;
using Shapes.Logic;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerShooter : Shooter
    {
        public float shootInterval = 0.1f;
        public ShootPattern shootPattern;

        public bool shooting;

        protected float lastShootTime;

        void Update()
        {
            if (shooting)
            {
                if (lastShootTime + shootInterval < Time.time)
                {
                    lastShootTime = Time.time;
                    shootPattern.shoot(this);
                }
            }
        }
    }
}