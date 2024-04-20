
using Shapes.Components;
using Shapes.Logic;
using Shapes.Logic.EntitySetterExt;
using Shapes.Logic.ShootPatterns;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.GameInst.Stages
{
    public class Stage1: EntitySetter
    {
        private Bullet lowMiniBullet, lowMedBullet, lowBallBullet;

        private ScheduleObject e1, e2, e3, e4,
            p1;

        public override void buildPrefabs()
        {
            lowMiniBullet = makeBullet(Prefabs.ammoMini, 30);
            lowMedBullet = makeBullet(Prefabs.ammoMedium, 40);
            lowBallBullet = makeBullet(Prefabs.ammoMedium, 32);

            var seriShoot = new SerialShoot { bullet = lowMiniBullet, shoots = 3, shootInterval = 0.1f };
            var triShoot = new BarrelsShoot{ shootBarrels = arr(pos(), deg(-6), pos(), deg(), pos(), deg(6)), bullets = arr(lowMiniBullet), shoots = 3, group = 3, shootInterval = 0.1f};

            var playerPos = GlobalVars.player.transform;
            e1 = makeWithTasks(Prefabs.fragEnemy, true,
                new MoveForwardTask{ duration = 4, moveSpeed = 40},
                new MoveForwardTask{ beginTime = 1, duration = 1, moveRot = deg(-90) },
                new TargetShootTask{ beginTime = 1, duration = 2, target = playerPos, shoots = 4, pattern = seriShoot }
            );
            e1.setHealth(10);

            e2 = makeWithTasks(Prefabs.fragEnemy, true,
                new MoveForwardTask{ duration = 4, moveSpeed = 40},
                new MoveForwardTask{ beginTime = 1, duration = 1, moveRot = deg(90) },
                new TargetShootTask{ beginTime = 1, duration = 2, target = playerPos, shoots = 4, pattern = seriShoot }
            );
            e2.setHealth(10);

            e3 = makeWithTasks(Prefabs.fragEnemy, true,
                new CurveMoveTask{ duration = 3, interp = Interp.POW3_OUT.f(), syncForward = true,
                    pathPoints = Math.bezireCurve(arr(pos(), pos(50, -10), pos(80, 0), pos(100, 20), pos(90, 40), pos(80, 20))) },
                new MovementTask{ beginTime = 3, duration = 0.5f, moveRot = rot(180) },
                new ActionTask<Hittable>{ beginTime = 3.5f, beginAct = e => e.damageable = true },
                new TargetShootTask{ beginTime = 3.5f, duration = 4, shoots = 6, target = playerPos, pattern = triShoot },
                new CurveMoveTask{ beginTime = 10, duration = 3, interp = Interp.POW3.f(), syncForward = true,
                    pathPoints = Math.bezireCurve(arr(pos(), pos(0, -40), pos(60, 10), pos(100, 20))) }
            );
            e3.setHealth(10);
            e3.setHittable(true, false);

            e4 = makeWithTasks(Prefabs.fragEnemy, true,
                new CurveMoveTask{ duration = 3, interp = Interp.POW3_OUT.f(), syncForward = true,
                    pathPoints = Math.bezireCurve(arr(pos(), pos(-50, -10), pos(-80, 0), pos(-100, 20), pos(-90, 40), pos(-80, 20))) },
                new MovementTask{ beginTime = 3, duration = 0.5f, moveRot = rot(180) },
                new ActionTask<Hittable>{ beginTime = 3.5f, beginAct = e => e.damageable = true },
                new TargetShootTask{ beginTime = 3.5f, duration = 4, shoots = 6, target = playerPos, pattern = triShoot },
                new CurveMoveTask{ beginTime = 10, duration = 3, interp = Interp.POW3.f(), syncForward = true,
                    pathPoints = Math.bezireCurve(arr(pos(), pos(0, -40), pos(-60, 10), pos(-100, 20))) }
            );
            e4.setHealth(10);
            e4.setHittable(true, false);

            var t = (BarrelsShoot) triShoot.clone();
            t.bullets = arr(lowMedBullet);
            t.shoots = 9;
            p1 = makeWithTasks(Prefabs.pieceEnemy, false,
                new MovementTask{ duration = 3, interp = Interp.POW3_OUT.f(), moveVec = pos(0, -40), noRot = true },
                new ActionTask<Hittable>{ beginTime = 2, beginAct = e => e.damageable = true },
                new TargetShootTask{ beginTime = 3, duration = -1, shoots = 1, target = playerPos, pattern = t },
                new MovementTask{ beginTime = 10, duration = 8, moveVec = pos(0, -100), interp = Interp.POW3.f(), noRot = true },
                new DestroyTask{ beginTime = 18 }
            );
            p1.setHittable(true, false);
        }

        public override void build()
        {
            world.worldBounds = new Rect(-20, 0, 120, 2000);
            world.viewPortBounds = new Rect(0, 0, 80, 80);
            world.viewPort = new Rect(0, 0, 60, 80);

            altY = world.playerHeight;
            world.schedule.addTasks(
                new WorldMoveTask{ from = 0, to = -world.worldBounds.height, duration = 60, beginTime = 0},
                new PositionGenerateTask{ beginTime = 3, duration = 3, generates = 7, genPosition = arr(pos(20, 110)), generatePrefabList = arr( e1 ), genRotation = arr( rot(180) )},
                new PositionGenerateTask{ beginTime = 9, duration = 3, generates = 7, genPosition = arr(pos(60, 110)), generatePrefabList = arr( e2 ), genRotation = arr( rot(180) )},
                new LinearPosGenerateTask{ beginTime = 15, duration = 2, generates = 4, fromPos = pos(-10, 45), toPos = pos(-43, 45), generatePrefabList = arr( e3 ) },
                new LinearPosGenerateTask{ beginTime = 15.25f, duration = 2, generates = 4, fromPos = pos(90, 45), toPos = pos(123, 45), generatePrefabList = arr( e4 ) },
                new PositionGenerateTask{ beginTime = 24, duration = 8, generates = 4, generateBatch = 2, genPosition = arr(pos(20, 90), pos(60, 90), pos(32, 110), pos(48, 110)), generatePrefabList = arr(p1), genRotation = arr(rot(180)) }
            );
            resetAlt();
        }
    }
}