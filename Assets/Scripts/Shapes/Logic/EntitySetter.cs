using Shapes.Components;
using Shapes.Logic;
using UnityEngine;

namespace Shapes.Logic
{
    public abstract class EntitySetter: MonoBehaviour
    {
        public World world;

        protected Vector3 alternativePos;

        protected float alternativeX
        {
            get => alternativePos.x;
            set => alternativePos.x = value;
        }
        protected float alternativeY
        {
            get => alternativePos.y;
            set => alternativePos.y = value;
        }
        protected float alternativeZ
        {
            get => alternativePos.z;
            set => alternativePos.z = value;
        }

        protected void resetAlt()
        {
            alternativePos = new Vector3(0, 0, 0);
        }

        private void Start()
        {
            buildPrefabs();
            buildWorld();
        }

        public abstract void buildPrefabs();

        public abstract void buildWorld();

        protected ScheduleObject makeWithTasks(ScheduleObject basePref, bool destroyEnd, params ScheduleTask[] tasks)
        {
            var res = basePref.makeInst().gameObject;
            res.SetActive(false);
            var sc = res.GetComponent<ScheduleObject>();
            sc.addTasks(tasks);
            sc.destroyOnEnd = destroyEnd;

            return sc;
        }

        protected ScheduleObject makeWithTasks(ScheduleObject basePref, params ScheduleTask[] tasks)
        {
            var res = basePref.makeInst().gameObject;
            res.SetActive(false);
            var sc = res.GetComponent<ScheduleObject>();
            sc.addTasks(tasks);

            return sc;
        }

        protected Bullet makeBullet(Bullet baseBullet, float speed)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;

            return res;
        }

        protected Bullet makeBullet(Bullet baseBullet, float speed, float damage)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;
            res.damage = damage;

            return res;
        }

        protected Bullet makeBullet(Bullet baseBullet, float speed, float damage, float lifeTime)
        {
            var obj = Instantiate(baseBullet.gameObject);
            obj.SetActive(false);
            var res = obj.GetComponent<Bullet>();
            res.speed = speed;
            res.damage = damage;
            res.lifetime = lifeTime;

            return res;
        }

        protected T[] arr<T>(params T[] elems)
        {
            return elems;
        }

        protected Vector3 pos(float x, float y, float z)
        {
            return new Vector3(x, y, z);
        }

        protected Vector3 pos(float x, float z)
        {
            return new Vector3(x, alternativeY, z);
        }

        protected Vector3 pos(float y)
        {
            return new Vector3(alternativeX, y, alternativeZ);
        }

        protected Vector3 pos()
        {
            return alternativePos;
        }

        protected Vector3 deg(float deg = 0)
        {
            return new Vector3(0, deg, 0);
        }

        protected Quaternion rot(float x, float y, float z)
        {
            return Quaternion.Euler(x, y, z);
        }

        protected Quaternion rot(float deg = 0)
        {
            return Quaternion.Euler(0, deg, 0);
        }
    }

    public static class Ext
    {
        public static Dropper drop(this ScheduleObject obj) => obj.GetComponent<Dropper>();
        public static void setHealth(this ScheduleObject obj, float health) => obj.GetComponent<Health>().maxHealth = health;
    }
}