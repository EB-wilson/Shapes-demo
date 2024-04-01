using UnityEngine;

namespace Shapes.Logic
{
    public class DirectionShootTask: ShootControlTask
    {
        public Quaternion shootBeginRotation;
        public Quaternion shootEndRotation;
        public bool absolute;

        private Quaternion beginRotation;
        private int shootCount;

        protected override void begin()
        {
            base.begin();
            beginRotation = shooter.transform.rotation;
            shootCount = 0;
        }

        protected override void action()
        {
            if (shootCount/shoots > progress) return;

            var shootRot = Quaternion.Lerp(shootBeginRotation, shootEndRotation, progress);

            if (absolute)
            {
                shooter.shootRotation = shootRot;
            }
            else
            {
                shooter.shootRotation = beginRotation * shootRot;
            }

            pattern.shoot(shooter);
            shootCount++;
        }
    }
}