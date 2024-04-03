using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class Dropper: Health
    {
        public Pickable[] dropList;
        public int drops;
        public int[] dropIndex;

        public float dropRotation;
        public float minDropSpeed = 10, maxDropSpeed = 20;

        protected override void doDestroy()
        {
            for (int i = 0; i < drops; i++)
            {
                var l = dropIndex[i % dropIndex.Length];
                var drop = dropList[l % dropList.Length];

                var ang = dropRotation + Random.Range(0f, 2*Mathf.PI);
                var speed = Random.Range(minDropSpeed, maxDropSpeed);
                var vel = new Vector3(
                    speed*Mathf.Cos(ang),
                    0,
                    speed*Mathf.Sin(ang)
                );

                var trans = transform;
                var inst = Instantiate(drop, trans.position, trans.rotation);
                Times.run(() => inst.controllable.motion.setVel(vel), 0);
            }
            base.doDestroy();
        }
    }
}