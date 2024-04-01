using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shapes.Components;
using Object = UnityEngine.Object;

namespace Shapes.Utils
{
    /// <summary>
    /// 此类型用于保存子弹实例的引用，被设计用于批量控制子弹的行为
    /// </summary>
    public class BulletGroup: IEnumerable<Bullet>
    {
        private List<Bullet> bullets = new();

        public int size => bullets.Count;

        public void destroy()
        {
            foreach (Bullet bullet in bullets)
            {
                Object.Destroy(bullet.gameObject);
            }
        }

        public void stop()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.motion.paused = true;
            }
        }

        public void start()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.motion.paused = false;
            }
        }

        public void add(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public void addAll(BulletGroup other)
        {
            bullets.AddRange(other.bullets);
        }

        public void remove(Bullet bullet)
        {
            bullets.Remove(bullet);
        }

        public Bullet first()
        {
            return bullets.First();
        }

        public void run(Action<Bullet> block)
        {
            foreach (var bullet in this)
            {
                block(bullet);
            }
        }

        public bool isEmpty()
        {
            return size == 0;
        }

        public bool any()
        {
            return size > 0;
        }

        public IEnumerator<Bullet> GetEnumerator()
        {
            return bullets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}