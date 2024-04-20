using System;
using Shapes.Logic;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class ShootWrap
    {
        public ShootPattern pattern;
        public float shootInterval;

        public float lastShootTime;

        public ShootWrap(ShootPattern pattern, float shootInterval)
        {
            this.pattern = pattern;
            this.shootInterval = shootInterval;
        }

        public virtual void update(Shooter shooter)
        {

            if (lastShootTime + shootInterval < Time.time)
            {
                lastShootTime = Time.time;
                pattern.shoot(shooter);
            }
        }
    }

    public class MultiWrap: ShootWrap
    {
        public ShootWrap[] wraps;

        public MultiWrap(params ShootWrap[] wraps) : base(null, 0)
        {
            this.wraps = wraps;
        }

        public override void update(Shooter shooter)
        {
            foreach (var wrap in wraps)
            {
                wrap.update(shooter);
            }
        }
    }

    public class PlayerShooter : Shooter
    {
        public ShootWrap[] shootWraps;

        public float minDamageScl = 0.5f;
        public float balanceMin = 0.2f;
        public float balanceMax = 0.65f;

        //public bool shooting;

        [NonSerialized] public bool isShift;

        public override float damageScl
        {
            get
            {
                var r = Mathf.Clamp01(Mathf.Max(GlobalVars.player.damageBalance(isShift) - balanceMin, 0)/(balanceMax - balanceMin));
                return Mathf.Clamp01(minDamageScl + r*minDamageScl);
            }
        }

        public void shooting()
        {
            shootWraps[Mathf.Min((int)GlobalVars.player.power, shootWraps.Length - 1)].update(this);
        }
    }
}