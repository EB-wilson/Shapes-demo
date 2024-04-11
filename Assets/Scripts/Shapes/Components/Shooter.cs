using System;
using Shapes.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Transform))]
    public class Shooter : MonoBehaviour
    {
        public int flag;
        public Vector3 shootOffset;

        public bool overrideDir;
        public Quaternion shootRotation;

        public virtual float damageScl => 1;

        public virtual void shoot(Bullet bullet, Vector3 offset, Quaternion shootDirOffset)
        {
            var rotation = overrideDir? shootRotation: transform.rotation;
            bullet.create(
                this,
                transform.position + rotation * (offset + shootOffset),
                damageScl,
                1,
                1,
                rotation * shootDirOffset
            ).gameObject.SetActive(true);
        }

    }
}