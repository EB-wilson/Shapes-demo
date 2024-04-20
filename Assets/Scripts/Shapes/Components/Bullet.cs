using System;
using System.Collections.Generic;
using Shapes.Utils;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Shapes.Components
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(Motion))]
    public class Bullet : ScheduleObject
    {
        public float damage;
        public int pierce;

        public float speed;
        public Quaternion direction;

        public Effect hitEffect;

        public Bullet fragmentBullet;
        public int frags = 1;
        public float fragSpeedSclMin = 1, fragSpeedSclMax = 1;
        public float fragLifeSclMin = 1, fragLifeSclMax = 1;
        public Vector3 fragSpreadRange = new(180, 180, 180);
        public bool fragOnHit = true;

        public int flag;

        [NonSerialized] public Motion motion;
        [NonSerialized] public PlayerController player;
        [NonSerialized] public Shooter owner;

        [NonSerialized] public bool outOfRanged;
        [NonSerialized] public bool grazed;

        private List<Hittable> collided = new();

        public Bullet create(Vector3 position, int flagP)
        {
            var res = (Bullet)makeInst();
            res.transform.position = position;
            res.flag = flagP;

            return res;
        }
        public Bullet create(Vector3 position, int flagP, Quaternion dir)
        {
            var res = (Bullet)makeInst();
            res.flag = flagP;
            res.transform.position = position;
            res.direction = dir;

            return res;
        }
        public Bullet create(Vector3 position, int flagP, float damageScl, float speedScl, Quaternion dir)
        {
            var res = (Bullet)makeInst();
            res.flag = flagP;
            res.transform.position = position;
            res.damage = damage*damageScl;
            res.speed = speed*speedScl;
            res.direction = dir;

            return res;
        }
        public Bullet create(Shooter ownerP, Vector3 position, Quaternion dir)
        {
            var res = (Bullet)makeInst();
            res.owner = ownerP;
            res.flag = ownerP.flag;
            res.transform.position = position;
            res.direction = dir;

            return res;
        }
        public Bullet create(Shooter ownerP, Vector3 position, float damageScl, float lifeScl, float speedScl, Quaternion dir)
        {
            var res = (Bullet)makeInst();
            res.owner = ownerP;
            res.flag = ownerP.flag;
            res.transform.position = position;
            res.damage = damage*damageScl;
            res.speed = speed*speedScl;
            res.direction = dir;

            return res;
        }

        private new void Start()
        {
            base.Start();
            motion = GetComponent<Motion>();
            player = GlobalVars.player.GetComponent<PlayerController>();
            motion.vel = direction*new Vector3(0, 0, speed);
        }

        private new void Update()
        {
            base.Update();

            GlobalVars.world.checkBullet(this);
            GlobalVars.world.checkBounds(transform);

            var playerHittable = player.current.hittable;
            if (!grazed && playerHittable.flag != flag && Vector3.Distance(playerHittable.transform.position, transform.position) <= 5)
            {
                grazed = true;
                playerHittable.onGraze(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var hittable = other.gameObject.GetComponent<Hittable>();
            if (outOfRanged || hittable == null || !hittable.hittable || hittable.flag == flag || collided.Contains(hittable)) return; //only hit hittable enemy
            hittable.onHit(this);
            if (hitEffect is not null)
            {
                var trans = transform;
                hitEffect.makeInst(trans.position, trans.rotation);
            }
            collided.Add(hittable);

            var health = other.gameObject.GetComponent<Health>();
            if (health != null && hittable.damageable)
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