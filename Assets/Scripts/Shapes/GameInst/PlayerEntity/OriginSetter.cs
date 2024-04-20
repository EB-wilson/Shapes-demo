using System;
using System.Collections;
using System.Collections.Generic;
using Shapes.Components;
using Shapes.GameInst;
using Shapes.Logic;
using Shapes.Logic.ShootPatterns;
using UnityEngine;

public class OriginSetter : EntitySetter
{
    private Bullet bullet, bulletRed, redHeigh;

    public override void buildPrefabs()
    {
        bullet = makeBullet(Prefabs.triBullet, 125,
            new RotSpeedControlTask{ toRotSpeed = 530 });
        bulletRed = makeBullet(Prefabs.triBulletRed, 145, 20,
            new RotSpeedControlTask{ toRotSpeed = 530 });
        redHeigh = makeBullet(Prefabs.triBulletRed, 158, 20,
            new RotSpeedControlTask{ toRotSpeed = 530 });
    }

    public override void build()
    {
        var control = GetComponent<PlayerControllable2D>();

        Func<Bullet, float, Bullet> curveBullet = (bul, dRot) =>
        {
            return makeBullet(bul, bul.speed, new SpeedRotateTask{ duration = -1, moveRot = deg(dRot) });
        };

        control.shooter.shootWraps = new ShootWrap[]
        {
            new(new BarrelsShoot{ shootBarrels = arr(pos(-2, 0), deg(), pos(), deg(), pos(2, 0), deg()), bullets = arr(bullet), shoots = 3, group = 3 }, 0.1f),
            new(new BarrelsShoot{ shootBarrels = arr(pos(-3, 0), deg(), pos(-1, 0), deg(), pos(1, 0), deg(), pos(3, 0), deg()), bullets = arr(bullet, bulletRed, bulletRed, bullet), shoots = 4, group = 4 }, 0.1f),
            new(new BarrelsShoot{ shootBarrels = arr(
                pos(-2, 0), deg(-6),
                pos(-2, 0), deg(),
                pos(), deg(),
                pos(2, 0), deg(),
                pos(2, 0), deg(6)
            ), bullets = arr(curveBullet(bullet, 32), bulletRed, redHeigh, bulletRed, curveBullet(bullet, -32)), shoots = 5, group = 5 }, 0.1f),
            new(new BarrelsShoot{ shootBarrels = arr(
                pos(-3, 0), deg(-12),
                pos(), deg(-8),
                pos(-2, 0), deg(),
                pos(), deg(),
                pos(2, 0), deg(),
                pos(), deg(8),
                pos(3, 0), deg(12)
            ), bullets = arr(curveBullet(bullet, 64), curveBullet(bullet, 56), redHeigh, redHeigh, redHeigh, curveBullet(bullet, -56), curveBullet(bullet, -64)), shoots = 7, group = 7 }, 0.1f),
            new(new BarrelsShoot{ shootBarrels = arr(
                pos(-3, 0), deg(-12),
                pos(-2, 0), deg(-8),
                pos(), deg(-10),
                pos(-2, 0), deg(),
                pos(), deg(),
                pos(2, 0), deg(),
                pos(), deg(10),
                pos(2, 0), deg(8),
                pos(3, 0), deg(12)
            ), bullets = arr(curveBullet(bullet, 64), curveBullet(bullet, 60), curveBullet(bulletRed, 36), redHeigh, redHeigh, redHeigh, curveBullet(bulletRed, -36), curveBullet(bullet, -60), curveBullet(bullet, -64)), shoots = 9, group = 9 }, 0.1f),
        };
    }
}
