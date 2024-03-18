using System;
using Shapes.Utils;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shapes.Components
{
    [RequireComponent(typeof(Collider), typeof(Motion))]
    public class Bullet : MonoBehaviour
    {
        public int flag;
        public float damage;
        public bool pierce;
        public float lifetime = 10;

        public float speed;
        public float time;
        public float direction;

        [NonSerialized] public Motion motion;

        void Start()
        {
            motion = GetComponent<Motion>();
            motion.vel = new Vector3(speed*Mathf.Cos(direction*Mathf.Deg2Rad), 0, speed*Mathf.Sin(direction*Mathf.Deg2Rad));
        }

        void Update()
        {
            time += Time.deltaTime;
            if (time >= lifetime)
            {
                Destroy(gameObject);
            }

            GlobalVars.world.checkBullet(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Hittable>() == null) return; //only hit hittable

            var health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.damage(damage);
            }

            if (!pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}