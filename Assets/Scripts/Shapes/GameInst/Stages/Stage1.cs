
using Shapes.Components;
using Shapes.Logic;
using Shapes.Logic.ShootPatterns;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.GameInst.Stages
{
    public class Stage1: EntitySetter
    {
        private Bullet lowMedBullet;

        private ScheduleObject e1, e2, e3, e4;

        public override void buildPrefabs()
        {
            lowMedBullet = makeBullet(Prefabs.ammoMedium, 40);

            var playerPos = GlobalVars.player.transform;
            e1 = makeWithTasks(Prefabs.fragEnemy, true,
                new MoveForwardTask{ duration = 4, moveSpeed = 40},
                new MoveForwardTask{ beginTime = 1, duration = 1, moveRot = deg(-90) },
                new TargetShootTask{ beginTime = 1, duration = 2, target = playerPos, shoots = 4,
                    pattern = new SerialShoot{ bullet = lowMedBullet, shoots = 3, shootInterval = 0.1f } }
            );
            e1.setHealth(10);

            e2 = makeWithTasks(Prefabs.fragEnemy, true,
                new MoveForwardTask{ duration = 4, moveSpeed = 40},
                new MoveForwardTask{ beginTime = 1, duration = 1, moveRot = deg(90) },
                new TargetShootTask{ beginTime = 1, duration = 2, target = playerPos, shoots = 4,
                    pattern = new SerialShoot{ bullet = lowMedBullet, shoots = 3, shootInterval = 0.1f } }
            );
            e2.setHealth(10);

            e3 = makeWithTasks(Prefabs.fragEnemy, true,
                new BezierMoveTask{ duration = 3, interp = Interp.POW3_OUT.f(), syncForward = true,
                    controlPoints = arr(pos(), pos(50, -10), pos(80, 0), pos(100, 20), pos(90, 40), pos(80, 20)) },
                new MovementTask{ beginTime = 3, duration = 0.5f, moveRot = rot(180) },
                new TargetShootTask{ beginTime = 3.5f, duration = 4, shoots = 6, target = playerPos,
                    pattern = new BarrelsShoot{ shootBarrels = arr(pos(), deg(-6), pos(), deg(), pos(), deg(6)), bullets = arr(lowMedBullet), shoots = 3, group = 3, shootInterval = 0.1f} },
                new BezierMoveTask{ beginTime = 10, duration = 3, interp = Interp.POW3.f(), syncForward = true,
                    controlPoints = arr(pos(), pos(0, -40), pos(60, 10), pos(100, 20)) }
            );
            e3.setHealth(10);

            e4 = makeWithTasks(Prefabs.fragEnemy, true,
                new BezierMoveTask{ duration = 3, interp = Interp.POW3_OUT.f(), syncForward = true,
                    controlPoints = arr(pos(), pos(-50, -10), pos(-80, 0), pos(-100, 20), pos(-90, 40), pos(-80, 20)) },
                new MovementTask{ beginTime = 3, duration = 0.5f, moveRot = rot(180) },
                new TargetShootTask{ beginTime = 3.5f, duration = 4, shoots = 6, target = playerPos,
                    pattern = new BarrelsShoot{ shootBarrels = arr(pos(), deg(-6), pos(), deg(), pos(), deg(6)), bullets = arr(lowMedBullet), shoots = 3, group = 3, shootInterval = 0.1f} },
                new BezierMoveTask{ beginTime = 10, duration = 3, interp = Interp.POW3.f(), syncForward = true,
                    controlPoints = arr(pos(), pos(0, -40), pos(-60, 10), pos(-100, 20)) }
            );
            e4.setHealth(10);
        }

        public override void buildWorld()
        {
            world.worldBounds = new Rect(-20, 0, 120, 2000);
            world.viewPortBounds = new Rect(0, 0, 80, 80);
            world.viewPort = new Rect(0, 0, 60, 80);

            alternativeY = world.playerHeight;
            world.schedule.addTasks(
                new WorldMoveTask{ from = 0, to = -2000, duration = 60, beginTime = 0},
                new PositionGenerateTask{ beginTime = 3, duration = 3, generates = 7, genPosition = arr(pos(20, 110)), generatePrefabList = arr( e1 ), genRotation = arr( rot(180) )},
                new PositionGenerateTask{ beginTime = 9, duration = 3, generates = 7, genPosition = arr(pos(60, 110)), generatePrefabList = arr( e2 ), genRotation = arr( rot(180) )},
                new LinearPosGenerateTask{ beginTime = 15, duration = 2, generates = 4, fromPos = pos(-10, 45), toPos = pos(-43, 45), generatePrefabList = arr( e3 ) },
                new LinearPosGenerateTask{ beginTime = 15.25f, duration = 2, generates = 4, fromPos = pos(90, 45), toPos = pos(123, 45), generatePrefabList = arr( e4 ) }
            );
            resetAlt();
        }
    }
}