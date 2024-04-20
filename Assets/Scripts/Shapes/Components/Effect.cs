using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Shapes.Components
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect: ScheduleObject
    {
        private ParticleSystem particle;

        private new void Start()
        {
            base.Start();
            particle = GetComponent<ParticleSystem>();
        }

        private new void Update()
        {
            base.Update();
            if (!particle.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}