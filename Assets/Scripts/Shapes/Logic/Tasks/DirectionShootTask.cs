using UnityEngine;

namespace Shapes.Logic
{
    public class DirectionShootTask: ShootControlTask
    {
        public Quaternion shootBeginRotation;
        public Quaternion shootEndRotation;
        public bool absolute;

        public Vector3 spreadRange;

        protected override void action()
        {
            var prog = duration < 0? interp(Mathf.Clamp01(time)): progress;
            var shootRot = Quaternion.Slerp(shootBeginRotation, shootEndRotation, prog);
            var off = Quaternion.Euler(
                Random.Range(-spreadRange.x, spreadRange.x),
                Random.Range(-spreadRange.y, spreadRange.y),
                Random.Range(-spreadRange.z, spreadRange.z)
            );

            if (absolute)
            {
                shooter.shootRotation = shootRot*off;
            }
            else
            {
                shooter.shootRotation = self.transform.rotation * shootRot * off;
            }
            base.action();
        }

        public override ScheduleTask clone()
        {
            return new DirectionShootTask{ duration = duration, beginTime = beginTime, interp = interp,
                pattern = pattern.clone(), shoots = shoots,
                shootBeginRotation = shootBeginRotation, shootEndRotation = shootEndRotation, absolute = absolute, spreadRange = spreadRange};
        }

        protected override void shoot()
        {
            pattern.shoot(shooter);
        }
    }
}