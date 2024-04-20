using System;
using Shapes.Components;
using Shapes.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Logic.ShootPatterns
{
    public class BarrelsShoot: ShootPattern
    {
        public Bullet[] bullets;
        /// <summary>
        /// 发射管信息列表，每两个向量为一组，前一个数据表示发射位置偏移，第二个表示发射角度的偏移
        /// </summary>
        public Vector3[] shootBarrels;
        public int group = 1;

        public override void shoot(Shooter shooter)
        {
            for (var i = 0; i < shoots; i++)
            {
                Times.run(() =>
                {
                    if(shooter.IsDestroyed()) return;
                    var n = shootCount*2 % shootBarrels.Length;

                    shooter.shoot(bullets[shootCount%bullets.Length], shooter.shootOffset + shootBarrels[n], Quaternion.Euler(shootBarrels[n + 1]));
                    shootCount++;
                }, firstInterval + (int)(i/(float)group)*shootInterval);
            }
        }

        public override ShootPattern clone()
        {
            return new BarrelsShoot{ shoots = shoots, firstInterval = firstInterval, shootInterval = shootInterval,
                shootBarrels = shootBarrels, bullets = bullets, group = group};
        }
    }
}