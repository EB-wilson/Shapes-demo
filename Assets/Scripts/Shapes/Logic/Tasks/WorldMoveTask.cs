using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public class WorldMoveTask: ScheduleTask
    {
        public float from;
        public float to;

        protected override void begin()
        {
        }

        protected override void action()
        {
            GlobalVars.world.worldBounds.y = Mathf.Lerp(from, to, progress);
        }

        protected override void post()
        {
        }
    }
}