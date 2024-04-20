using Shapes.Components;
using Shapes.Logic;
using Shapes.Logic.ShootPatterns;
using UnityEngine;

namespace Shapes.GameInst.PlayerEntity
{
    public class FragmentSetter : EntitySetter
    {
        private Bullet bullet, bulletRed, redHeigh;

        public override void buildPrefabs()
        {
            bullet = makeBullet(Prefabs.glassBullet, 125,
                new RotSpeedControlTask{ toRotSpeed = 530 });
            bulletRed = makeBullet(Prefabs.glassBulletRed, 145, 18,
                new RotSpeedControlTask{ toRotSpeed = 530 });
            redHeigh = makeBullet(Prefabs.glassBulletRed, 158, 18,
                new RotSpeedControlTask{ toRotSpeed = 530 });
        }

        public override void build()
        {
            var control = GetComponent<PlayerControllable2D>();

            control.shooter.shootWraps = new ShootWrap[]
            {
                new(new BarrelsShoot{ shootBarrels = arr(pos(), deg(-3), pos(), deg(), pos(), deg(3)), bullets = arr(bullet), shoots = 3, group = 3 }, 0.1f),
                new(new BarrelsShoot{ shootBarrels = arr(pos(), deg(-9), pos(), deg(-2), pos(), deg(2), pos(), deg(9)), bullets = arr(bullet, bulletRed, bulletRed, bullet), shoots = 4, group = 4 }, 0.1f),
                new(new BarrelsShoot{
                    shootBarrels = arr(
                        pos(-1, 0), deg(-12),
                        pos(), deg(-4),
                        pos(), deg(),
                        pos(), deg(4),
                        pos(1, 0), deg(12)
                    ),
                    bullets = arr(bullet, bulletRed, bulletRed, bulletRed, bullet), shoots = 5, group = 5
                }, 0.1f),
                new(new BarrelsShoot{
                    shootBarrels = arr(
                        pos(-2, 0), deg(-14),
                        pos(), deg(-6),
                        pos(), deg(-3),
                        pos(), deg(),
                        pos(), deg(3),
                        pos(), deg(6),
                        pos(2, 0), deg(14)
                    ),
                    bullets = arr(bullet, bullet, bulletRed, redHeigh, bulletRed, bullet, bullet), shoots = 7, group = 7
                }, 0.1f),
                new(new BarrelsShoot{
                    shootBarrels = arr(
                        pos(-2, 0), deg(-16),
                        pos(-2, 0), deg(-13),
                        pos(), deg(-8),
                        pos(), deg(-3),
                        pos(), deg(),
                        pos(), deg(3),
                        pos(), deg(8),
                        pos(2, 0), deg(13),
                        pos(2, 0), deg(16)
                    ),
                    bullets = arr(bullet, bullet, bulletRed, redHeigh, redHeigh, redHeigh, bulletRed, bullet, bullet), shoots = 9, group = 9
                }, 0.1f),
            };
        }
    }
}