using UnityEngine;

namespace Shapes.Logic
{
    public class TargetShootTask: ShootControlTask
    {
        public Transform target;
        public Vector3 spreadRange;

        protected override void action()
        {
            var shootRot = Quaternion.LookRotation(target.position - transform.position);
            var off = Quaternion.Euler(
                Random.Range(-spreadRange.x, spreadRange.x),
                Random.Range(-spreadRange.y, spreadRange.y),
                Random.Range(-spreadRange.z, spreadRange.z)
            );

            shooter.shootRotation = shootRot * off;

            base.action();
        }

        protected override void shoot()
        {
            pattern.shoot(shooter);
        }
    }
}