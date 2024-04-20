using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Collider))]
    public abstract class Hittable : MonoBehaviour
    {
        public int flag;
        public bool hittable = true;
        public bool damageable = true;

        public abstract void onHit(Bullet bullet);
        public abstract void onDeath();
    }
}