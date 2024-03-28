using System;
using UnityEngine;

namespace Shapes.Logic
{
    public class WorldMoveTask: Task
    {
        public float from;
        public float to;

        public World world;

        void Start()
        {
            if (world == null)
            {
                world = GetComponent<World>();
            }
        }

        protected override void begin()
        {
        }

        protected override void action()
        {
            world.worldBounds.y = Mathf.Lerp(from, to, progress);
        }

        protected override void post()
        {
        }
    }
}