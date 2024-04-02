using System;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerControllable2D : Controllable2D
    {
        public float balanceOffSpeed = 0.1f;

        [NonSerialized] public PlayerShooter shooter;
        [NonSerialized] public PlayerHittable hittable;

        private void Start()
        {
            motion = GetComponent<Motion>();
            shooter = gameObject.GetComponent<PlayerShooter>();
            hittable = gameObject.GetComponent<PlayerHittable>();
        }
    }
}