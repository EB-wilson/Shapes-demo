using System;
using UnityEngine;

namespace Shapes.Components
{
    /// <summary>
    /// 该组件为实体提供直接操控速度的运动功能，为有效的高精度平滑运动控制提供基础。
    /// 注意：该组件仅提供基础的运动控制，不提供任何物理特性。不应当与<see cref="Rigidbody"/>结合使用
    /// </summary>
    [RequireComponent(typeof(Transform))]
    public class Motion : MonoBehaviour
    {
        public Vector3 vel;
        public bool facingVelDir;
        public bool paused;

        [NonSerialized] public Transform selfPos;

        public float speed
        {
            get { return vel.magnitude; }
            set {
                var v = vel.normalized;
                if (v.magnitude <= 0) { v = selfPos.rotation * new Vector3(1, 0, 0); }

                vel = v * value;
            }
        }

        void Start()
        {
            selfPos = transform;
        }

        void Update()
        {
            if (paused) return;

            selfPos.position += vel*Time.deltaTime;
            if (facingVelDir)
            {
                selfPos.forward = selfPos.position + vel;
            }
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
            selfPos.position += vec;
        }

        public void setVel(float sx, float sy, float sz)
        {
            setVel(sx, sy, sz);
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