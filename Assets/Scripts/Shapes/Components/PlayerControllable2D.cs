using System;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerControllable2D : Controllable2D
    {
        public float balanceOffSpeed = 0.02f;
        public float shootingBalanceOffSpeed = 0.05f;

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