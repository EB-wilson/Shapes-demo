using System;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerControllable2D : Controllable2D
    {
        [NonSerialized] public PlayerShooter shooter;

        private void Start()
        {
            motion = GetComponent<Motion>();
            shooter = gameObject.GetComponent<PlayerShooter>();
        }
    }
}