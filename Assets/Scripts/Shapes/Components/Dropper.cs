using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class Dropper: EnemyHittable
    {
        public Pickable[] dropList;
        public int drops;
        public int[] dropIndex;

        public float dropRotation;
        public float dropSpread;
        public float dropRandSpread;
        public float minDropSpeed = 10, maxDropSpeed = 20;

        public override void onDeath()
        {
            base.onDeath();

            for (var i = 0; i < drops; i++)
            {
                var l = dropIndex == null || dropIndex.Length == 0? i: dropIndex[i % dropIndex.Length];
                var drop = dropList[l % dropList.Length];

                var ang = (dropRotation + dropSpread*i + Random.Range(-dropRandSpread, dropRandSpread))*Mathf.Deg2Rad;
                var speed = Random.Range(minDropSpeed, maxDropSpeed);
                var vel = new Vector3(
                    speed*Mathf.Cos(ang),
                    0,
                    speed*Mathf.Sin(ang)
                );

                var trans = transform;
                var inst = Instantiate(drop, trans.position, trans.rotation);
                Times.run(() => inst.motion.setVel(vel), 0);
            }
        }
    }
}