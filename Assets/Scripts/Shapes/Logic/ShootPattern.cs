using Shapes.Components;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public class ShootPattern: MonoBehaviour
    {
        public Bullet bulletType;

        public BulletGroup shoot(Shooter shooter)
        {
            BulletGroup g = new BulletGroup();

            for (int i = 0; i < 5; i++)
            {
                int n = i - 2;
                Bullet bull = Instantiate(bulletType);
                bull.flag = shooter.flag;
                var position = shooter.transform.position;
                bull.transform.position = new Vector3(position.x + n/2f, position.y, position.z - Mathf.Abs(n/4f));
                bull.direction = 90;
                g.add(bull);
            }

            return g;
        }
    }
}