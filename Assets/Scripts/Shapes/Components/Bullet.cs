using System;
using System.Collections.Generic;
using Shapes.Utils;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
        public Quaternion direction;

        public Bullet fragmentBullet;
        public int frags = 1;
        public float fragSpeedSclMin = 1, fragSpeedSclMax = 1;
        public float fragLifeSclMin = 1, fragLifeSclMax = 1;
        public Vector3 fragSpreadRange = new(180, 180, 180);
        public bool fragOnHit = true;

        [NonSerialized] public float time;
        [NonSerialized] public Motion motion;
        [NonSerialized] public Shooter owner;

        private List<Hittable> collided = new();

        public Bullet create(Vector3 position, int flagP)
        {
            var res = Instantiate(this);
            res.transform.position = position;
            res.flag = flagP;

            return res;
        }
        public Bullet create(Vector3 position, int flagP, Quaternion dir)
        {
            var res = Instantiate(this);
            res.flag = flagP;
            res.transform.position = position;
            res.direction = dir;

            return res;
        }
        public Bullet create(Vector3 position, int flagP, float damageScl, float lifeScl, float speedScl, Quaternion dir)
        {
            var res = Instantiate(this);
            res.flag = flagP;
            res.transform.position = position;
            res.damage = damage*damageScl;
            res.lifetime = lifetime*lifeScl;
            res.speed = speed*speedScl;
            res.direction = dir;

            return res;
        }
        public Bullet create(Shooter ownerP, Vector3 position, Quaternion dir)
        {
            var res = Instantiate(this);
            res.owner = ownerP;
            res.flag = ownerP.flag;
            res.transform.position = position;
            res.direction = dir;

            return res;
        }
        public Bullet create(Shooter ownerP, Vector3 position, float damageScl, float lifeScl, float speedScl, Quaternion dir)
        {
            var res = Instantiate(this);
            res.owner = ownerP;
            res.flag = ownerP.flag;
            res.transform.position = position;
            res.damage = damage*damageScl;
            res.lifetime = lifetime*lifeScl;
            res.speed = speed*speedScl;
            res.direction = dir;

            return res;
        }

        private void Start()
        {
            motion = GetComponent<Motion>();
            motion.vel = direction*new Vector3(0, 0, speed);
        }

        private void Update()
        {
            time += Time.deltaTime;
            if (time >= lifetime)
            {
                Destroy(gameObject);
            }

            GlobalVars.world.checkBullet(transform);
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

            if (fragOnHit)
            {
                creatFragments();
            }

            if (pierce < 0 || collided.Count <= pierce) return;

            Destroy(gameObject);

            if (!fragOnHit)
            {
                creatFragments();
            }
        }

        protected virtual void creatFragments()
        {
            if (fragmentBullet == null) return;

            for (var i = 0; i < frags; i++)
            {
                var angleOff = new Vector3(
                    Random.Range(-fragSpreadRange.x, fragSpreadRange.x),
                    Random.Range(-fragSpreadRange.y, fragSpreadRange.y),
                    Random.Range(-fragSpreadRange.z, fragSpreadRange.z)
                );

                fragmentBullet.create(owner,
                    transform.position,
                    1,
                    Random.Range(fragSpeedSclMin, fragSpeedSclMax),
                    Random.Range(fragLifeSclMin, fragLifeSclMax),
                    Quaternion.Euler(angleOff)*transform.rotation
                );
            }
        }
    }
}