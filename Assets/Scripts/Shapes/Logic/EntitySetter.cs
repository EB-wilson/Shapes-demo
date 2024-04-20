using Shapes.Components;
using Shapes.Logic;
using UnityEngine;

namespace Shapes.Logic
{
    public abstract class EntitySetter: MonoBehaviour
    {
        public World world;

        protected static Vector3 alternativePos;

        protected static float altX
        {
            get => alternativePos.x;
            set => alternativePos.x = value;
        }
        protected static float altY
        {
            get => alternativePos.y;
            set => alternativePos.y = value;
        }
        protected static float altZ
        {
            get => alternativePos.z;
            set => alternativePos.z = value;
        }

        protected static void resetAlt()
        {
            alternativePos = new Vector3(0, 0, 0);
        }

        private void Start()
        {
            buildPrefabs();
            build();
        }

        public abstract void buildPrefabs();

        public abstract void build();

        protected static ScheduleObject makeWithTasks(ScheduleObject basePref, bool destroyEnd, params ScheduleTask[] tasks)
        {
            var res = basePref.makeInst();
            res.gameObject.SetActive(false);
            res.addTasks(tasks);
            res.destroyOnEnd = destroyEnd;

            return res;
        }

        protected static ScheduleObject makeWithTasks(ScheduleObject basePref, params ScheduleTask[] tasks)
        {
            var res = basePref.makeInst();
            res.gameObject.SetActive(false);
            res.addTasks(tasks);

            return res;
        }

        protected static Bullet makeBullet(Bullet baseBullet, float speed, params ScheduleTask[] tasks)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;
            res.addTasks(tasks);

            return res;
        }

        protected static Bullet makeBullet(Bullet baseBullet, float speed, float damage, params ScheduleTask[] tasks)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;
            res.damage = damage;
            res.addTasks(tasks);

            return res;
        }

        protected static Bullet makeBullet(Bullet baseBullet, float speed, float damage, float lifeTime, params ScheduleTask[] tasks)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;
            res.damage = damage;
            res.addTasks(tasks);
            res.addTask(new DestroyTask{ beginTime = lifeTime });

            return res;
        }

        protected static T[] arr<T>(params T[] elems)
        {
            return elems;
        }

        protected static Vector3 pos(float x, float y, float z)
        {
            return new Vector3(x, y, z);
        }

        protected static Vector3 pos(float x, float z)
        {
            return new Vector3(x, altY, z);
        }

        protected static Vector3 pos(float y)
        {
            return new Vector3(altX, y, altZ);
        }

        protected static Vector3 pos()
        {
            return alternativePos;
        }

        protected static Vector3 deg(float deg = 0)
        {
            return new Vector3(0, deg, 0);
        }

        protected static Vector3 deg(float rx, float rz)
        {
            return new Vector3(rx, 0, rz);
        }

        protected static Quaternion rot(float x, float y, float z)
        {
            return Quaternion.Euler(x, y, z);
        }

        protected static Quaternion rot(float deg = 0)
        {
            return Quaternion.Euler(0, deg, 0);
        }
    }

    namespace EntitySetterExt
    {
        public static class Ext
            {
                public static Dropper drop(this ScheduleObject obj) => obj.GetComponent<Dropper>();
                // ReSharper disable Unity.PerformanceAnalysis
                public static void setHealth(this ScheduleObject obj, float health) => obj.GetComponent<Health>().maxHealth = health;

                // ReSharper disable Unity.PerformanceAnalysis
                public static void setHittable(this ScheduleObject obj, bool hittable, bool damageable)
                {
                    var hit = obj.GetComponent<Hittable>();
                    hit.hittable = hittable;
                    hit.damageable = damageable;
                }
            }
    }
}