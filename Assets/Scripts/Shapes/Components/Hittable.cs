using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Collider))]
    public abstract class Hittable : MonoBehaviour
    {
        public int flag;

        private void OnCollisionEnter(Collision other)
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet == null) return; //cannot hit by non bullet entity

            if (flag != bullet.flag) onHit(bullet);
        }

        public abstract void onHit(Bullet bullet);
    }
}