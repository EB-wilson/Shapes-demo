using Shapes.Components;
using Shapes.Logic;
using Shapes.Logic.ShootPatterns;
using UnityEngine;

namespace Shapes.GameInst.PlayerEntity
{
    public class FragmentSetter : EntitySetter
    {
        public override void buildPrefabs()
        {

        }

        public override void buildWorld()
        {
            var control = GetComponent<PlayerControllable2D>();

            control.shooter.shootWraps = new ShootWrap[]
            {
                new(new BarrelsShoot{ shootBarrels = arr(pos(), deg(-6), pos(), deg(), pos(), deg(6)), bullets = arr(Prefabs.ammoMedium), shoots = 3, group = 3 }, 0.1f)
            };
        }
    }
}