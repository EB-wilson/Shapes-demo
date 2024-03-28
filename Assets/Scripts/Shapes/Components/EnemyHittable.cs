using System;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Health))]
    public class EnemyHittable: Hittable
    {
        [NonSerialized] public Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        public override void onHit(Bullet bullet)
        {

        }
    }
}