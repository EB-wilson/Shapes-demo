using System;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Motion))]
    public class Controllable2D : MonoBehaviour
    {
        public float speed = 10.0f;
        public float hasten = -1;
        public float drag = -1;

        [NonSerialized] public Motion motion;
        [NonSerialized] public bool moving;

        private void Start()
        {
            motion = GetComponent<Motion>();
        }

        void Update()
        {
            clamp();
            if (moving) { moving = false; }
            else
            {
                float rat = Mathf.Min(Time.deltaTime*drag, motion.vel.magnitude);

                if (drag > 0) motion.hasten(-rat*(motion.vel).normalized);
                else motion.setVel(Vector3.zero);
            }
        }

        /// <summary>
        /// 采用角度实现万向移动控制以便后续可能需要对手柄编写的支持
        /// </summary>
        /// <param name="angle">移动方向的角度</param>
        /// <param name="lerp">移动速度的插值，默认为1</param>
        public void move(float angle, float lerp = 1)
        {
            //Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            var has = hasten * Time.deltaTime;

            float dx = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dz = Mathf.Sin(angle * Mathf.Deg2Rad);

            if (hasten > 0) motion.hasten(has*new Vector3(dx, 0, dz));
            else motion.setVel(speed*lerp*new Vector3(dx, 0, dz));

            moving = true;
            clamp(lerp);
        }

        protected void clamp(float lerp = 1)
        {
            motion.vel = Vector3.ClampMagnitude(motion.vel, speed*lerp);
        }
    }
}