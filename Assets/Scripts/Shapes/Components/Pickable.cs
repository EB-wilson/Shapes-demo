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

    [RequireComponent(typeof(Controllable2D))]
    public class Pickable: MonoBehaviour
    {
        public DropType type;
        public int data;
        public float fdata;

        public Controllable2D controllable;
        public PlayerController controller;

        private void Start()
        {
            controllable = GetComponent<Controllable2D>();
            controller = GlobalVars.player.GetComponent<PlayerController>();
        }

        private void Update()
        {
            GlobalVars.world.checkBullet(transform);

            var player = controller.current.hittable;
            var dst = Vector3.Distance(player.transform.position, transform.position);

            if (dst <= player.adsorptionRange)
            {
                var v = player.transform.position - transform.position;
                controllable.move(Math.angle(v.x, v.y));

                if (dst > player.pickupRange) return;

                player.onPickup(this);
                Destroy(gameObject);
            }
            else
            {
                controllable.move(-90, 0.5f);
            }
        }
    }
}