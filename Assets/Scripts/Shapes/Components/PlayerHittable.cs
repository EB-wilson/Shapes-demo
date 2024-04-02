using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerHittable : Hittable
    {
        public float adsorptionRange = 8f;
        public float pickupRange = 2f;

        public void onPickup(Pickable pickable)
        {
            switch (pickable.type)
            {
                case DropType.SCORE_POINT:
                {
                    GlobalVars.player.score += pickable.fdata;
                    GlobalVars.player.health += pickable.fdata * 0.00001f;
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

        public override void onHit(Bullet bullet)
        {
            GlobalVars.player.health -= 10;
            GlobalVars.player.miss++;
        }
    }
}