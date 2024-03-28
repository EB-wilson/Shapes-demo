using System;
using System.Collections.Generic;
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
        public int pierce;
        public float lifetime = 10;

        public float speed;
        public float direction;

        [NonSerialized] public float time;
        [NonSerialized] public Motion motion;
        [NonSerialized] public Shooter owner;

        private List<Hittable> collided = new();

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

        private void OnTriggerEnter(Collider other)
        {
            var hittable = other.gameObject.GetComponent<Hittable>();
            if (hittable == null || hittable.flag == flag || collided.Contains(hittable)) return; //only hit hittable enemy
            hittable.onHit(this);
            collided.Add(hittable);

            var health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.damage(damage);
            }

            if (pierce >= 0 && collided.Count > pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}