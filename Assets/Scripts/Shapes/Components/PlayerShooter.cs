using System;
using Shapes.Logic;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerShooter : Shooter
    {
        public float shootInterval = 0.1f;
        public ShootPattern shootPattern;

        public float minDamageScl = 0.4f;
        public float balanceMin = 0.2f;
        public float balanceMax = 0.6f;

        public bool shooting;

        [NonSerialized] public bool isShift;
        protected float lastShootTime;

        public override float damageScl
        {
            get
            {
                var r = Mathf.Clamp01(Mathf.Max(GlobalVars.player.damageBalance(isShift) - balanceMin, 0)/(balanceMax - balanceMin));
                return Mathf.Clamp01(minDamageScl + r*(1 - minDamageScl));
            }
        }

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