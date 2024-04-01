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

        public Transform shootPos;
        public bool overrideDir;

        public Quaternion shootRotation;

        private void Start()
        {
            if (shootPos == null)
            {
                shootPos = transform;
            }
            else
            {
                overrideDir = true;
            }
        }

        public virtual void shoot(Bullet bullet, Vector3 offset, Quaternion shootDirOffset)
        {
            var rotation = overrideDir? shootRotation: shootPos.rotation;
            bullet.create(
                this,
                shootPos.position + rotation * (offset + shootOffset),
                shootDirOffset * rotation
            );
        }
    }
}