using System;
using UnityEngine;

namespace Shapes.Components
{
    /// <summary>
    /// 该组件为实体提供直接操控速度的运动功能，为有效的高精度平滑运动控制提供基础。
    /// 注意：该组件仅提供基础的运动控制，不提供任何物理特性。不应当在采用动量的同时仍使用<see cref="Rigidbody"/>控制运动，这会产生一些很奇怪的问题。
    /// </summary>
    public class Motion : MonoBehaviour
    {
        public Vector3 vel;
        public bool facingVelDir;
        public bool paused;

        public float speed
        {
            get => vel.magnitude;
            set {
                var v = vel.normalized;
                if (v.magnitude <= 0) { v = transform.rotation * new Vector3(1, 0, 0); }

                vel = v * value;
            }
        }

        void Start()
        {
            var rigidBody = GetComponent<Rigidbody>();

            if (rigidBody == null) return;
            rigidBody.useGravity = false;
            rigidBody.drag = 0;
            rigidBody.angularDrag = 0;
        }

        private void Update()
        {
            if (paused) return;

            transform.position += vel * Time.deltaTime;

            if (!facingVelDir) return;
            var trans = transform;
            trans.LookAt(trans.position + vel);
        }

        public void stop()
        {
            vel = new Vector3(0, 0, 0);
        }

        public void move(float dx, float dy, float dz)
        {
            move(new Vector3(dx, dy, dz));
        }

        public void move(Vector3 vec)
        {
            transform.position += vec;
        }

        public void setVel(float sx, float sy, float sz)
        {
            setVel(new Vector3(sx, sy, sz));
        }

        public void setVel(Vector3 vec)
        {
            vel = vec;
        }

        public void hasten(float ax, float ay, float az)
        {
            hasten(new Vector3(ax,ay, az));
        }

        public void hasten(Vector3 vec)
        {
            vel += vec;
        }
    }
}