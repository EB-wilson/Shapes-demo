using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Health))]
    public class EnemyHittable: Hittable
    {
        public GameObject deathEffect;
        public int killScore = 12000;
        public int hitScore = 71;

        [NonSerialized] public Health health;

        private bool deathEffectNotNull;

        private void Start()
        {
            deathEffectNotNull = deathEffect != null;
            health = GetComponent<Health>();
        }

        public override void onHit(Bullet bullet)
        {
            GlobalVars.player.score += hitScore;
        }

        public override void onDeath()
        {
            GlobalVars.player.score += killScore;

            if (deathEffectNotNull)
            {
                var trans = transform;
                Instantiate(deathEffect, trans.position, trans.rotation);
            }
        }
    }
}