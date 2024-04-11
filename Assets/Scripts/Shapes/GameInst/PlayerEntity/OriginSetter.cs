using System.Collections;
using System.Collections.Generic;
using Shapes.Components;
using Shapes.GameInst;
using Shapes.Logic;
using Shapes.Logic.ShootPatterns;
using UnityEngine;

public class OriginSetter : EntitySetter
{
    public override void buildPrefabs()
    {

    }

    public override void buildWorld()
    {
        var control = GetComponent<PlayerControllable2D>();

        control.shooter.shootWraps = new ShootWrap[]
        {
            new(new BarrelsShoot{ shootBarrels = arr(pos(-2, 0), deg(), pos(), deg(), pos(2, 0), deg()), bullets = arr(Prefabs.ammoMedium), shoots = 3, group = 3 }, 0.1f)
        };
    }
}
