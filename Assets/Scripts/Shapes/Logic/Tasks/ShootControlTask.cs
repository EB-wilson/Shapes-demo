using System;
using Shapes.Components;
using UnityEngine;

namespace Shapes.Logic
{
    public abstract class ShootControlTask: ScheduleTask
    {
        /// <summary>
        /// 在任务周期内的发射次数，如果此任务是循环或者无期限的，此数据表示在一秒内的发射次数
        /// </summary>
        public float shoots;
        public ShootPattern pattern;

        public Shooter shooter;

        private bool isOverride;
        private int shootCount;

        private void Start()
        {
            if (shooter != null) return;

            var sh = GetComponent<Shooter>();
            if (sh == null) sh = GetComponent<PlayerShooter>();

            shooter = sh;
            isOverride = sh.overrideDir;
        }

        public override void reset()
        {
            base.reset();
            isOverride = false;
            shootCount = 0;
        }

        protected override void begin()
        {
            shooter.overrideDir = true;
        }

        protected override void action()
        {
            var prog = duration < 0? interp(Mathf.Clamp01(time)): progress;

            if (shootCount / shoots < prog)
            {
                shoot();
                shootCount++;
            }

            if (duration > 0 || prog < 1) return;
            time = 0;
            shootCount = 0;
        }

        protected override void post()
        {
            shooter.overrideDir = isOverride;
        }

        protected abstract void shoot();
    }
}