using Shapes.Components;
using UnityEngine;

namespace Shapes.GameInst
{
    public static class Prefabs
    {
        public static ScheduleObject fragEnemy = Resources.Load<ScheduleObject>("Prefabs/Entity/Frag-Enemy");

        public static Bullet ammoMedium = Resources.Load<Bullet>("Prefabs/Bullet/AmmoMedium");
    }
}