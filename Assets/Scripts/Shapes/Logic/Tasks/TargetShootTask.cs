using UnityEngine;

namespace Shapes.Logic
{
    public class TargetShootTask: ShootControlTask
    {
        public Transform target;
        public Vector3 spreadRange;

        protected override void action()
        {
            var shootRot = Quaternion.LookRotation(target.position - self.transform.position);
            var off = Quaternion.Euler(
                Random.Range(-spreadRange.x, spreadRange.x),
                Random.Range(-spreadRange.y, spreadRange.y),
                Random.Range(-spreadRange.z, spreadRange.z)
            );

            shooter.shootRotation = shootRot * off;

            base.action();
        }

        public override ScheduleTask clone()
        {
            return new TargetShootTask{ duration = duration, beginTime = beginTime, interp = interp,
                pattern = pattern.clone(), shoots = shoots,
                target = target, spreadRange = spreadRange };
        }

        protected override void shoot()
        {
            pattern.shoot(shooter);
        }
    }
}