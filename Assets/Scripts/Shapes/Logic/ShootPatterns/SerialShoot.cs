using Shapes.Components;
using Shapes.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Logic
{
    public class SerialShoot: ShootPattern
    {
        public Bullet bullet;

        public override void shoot(Shooter shooter)
        {
            for (var i = 0; i < shoots; i++)
            {
                Times.run(() =>
                {
                    if(shooter.IsDestroyed()) return;
                    shooter.shoot(bullet, Vector3.zero, Quaternion.identity);
                    shootCount++;
                }, i*shootInterval);
            }
        }
    }
}