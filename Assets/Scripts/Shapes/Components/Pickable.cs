using System;
using Shapes.Utils;
using UnityEngine;
using Math = Shapes.Utils.Math;

namespace Shapes.Components
{
    public enum DropType
    {
        SCORE_POINT,
        POWER,
        BOMB,
        SHIELD
    }

    [RequireComponent(typeof(Motion))]
    public class Pickable: MonoBehaviour
    {
        public DropType type;
        public int data;
        public float fdata;
        public float dropSpeed = 60;
        public float pickSpeed = 80;

        [NonSerialized] public Motion motion;
        [NonSerialized] public PlayerController controller;
        private bool picked;

        private void Start()
        {
            motion = GetComponent<Motion>();
            controller = GlobalVars.player.GetComponent<PlayerController>();
        }

        private void Update()
        {
            GlobalVars.world.checkBounds(transform);

            var player = controller.current.hittable;
            var dst = Vector3.Distance(player.transform.position, transform.position);

            if (picked || dst <= player.adsorptionRange || player.onPickupLine())
            {
                picked = true;
                var v = (player.transform.position - transform.position).normalized;
                var vel = Mathf.Lerp(motion.vel.magnitude, pickSpeed, Time.deltaTime);
                motion.vel = v * vel;

                if (dst > player.pickupRange) return;

                player.onPickup(this);
                Destroy(gameObject);
            }
            else
            {
                motion.vel = Vector3.Lerp(motion.vel, new Vector3(0, 0, -dropSpeed), Time.deltaTime);
            }
        }
    }
}