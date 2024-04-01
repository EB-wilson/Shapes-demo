using System;
using Shapes.Components;

namespace Shapes.Logic
{
    public abstract class ShootControlTask: ScheduleTask
    {
        public float shoots;
        public ShootPattern pattern;

        public Shooter shooter;

        private bool isOverride;

        private void Start()
        {
            if (shooter != null) return;

            var sh = GetComponent<Shooter>();
            if (sh == null) sh = GetComponent<PlayerShooter>();

            shooter = sh;
            isOverride = sh.overrideDir;
        }

        protected override void begin()
        {
            shooter.overrideDir = true;
        }

        protected override void post()
        {
            shooter.overrideDir = isOverride;
        }
    }
}