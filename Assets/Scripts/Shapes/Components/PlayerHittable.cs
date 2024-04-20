using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerHittable : Hittable
    {
        public Effect grazeEffect;
        [Range(0, 1)] public float pickupLine = 1;
        public float adsorptionRange = 8f;
        public float pickupRange = 2f;

        public virtual void onPickup(Pickable pickable)
        {
            switch (pickable.type)
            {
                case DropType.SCORE_POINT:
                {
                    GlobalVars.player.score += pickable.data;
                    GlobalVars.player.health += pickable.data * 0.0001f;
                    break;
                }
                case DropType.POWER:
                {
                    GlobalVars.player.power += pickable.fdata;
                    break;
                }
                case DropType.BOMB:
                {
                    GlobalVars.player.bombs += pickable.data;
                    break;
                }
                case DropType.SHIELD:
                {
                    throw new NotImplementedException();
                }
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void onGraze(Bullet bullet)
        {
            if (grazeEffect is not null)
            {
                var trans = transform;
                grazeEffect.makeInst(trans.position, trans.rotation);
            }

            GlobalVars.player.graze++;
            GlobalVars.player.score += 967;
        }

        public override void onHit(Bullet bullet)
        {
            GlobalVars.player.health = Mathf.Max(0, GlobalVars.player.health - 12);
            GlobalVars.player.power = Mathf.Max(0, GlobalVars.player.power - 1);
            GlobalVars.player.miss++;
        }

        public override void onDeath(){}

        public virtual bool onPickupLine()
        {
            return pickupLine < 1 && transform.position.z >= GlobalVars.world.viewPortBounds.height * pickupLine;
        }
    }
}