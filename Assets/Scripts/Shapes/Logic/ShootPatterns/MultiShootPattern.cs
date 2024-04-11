using System.Collections.Generic;
using Shapes.Components;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic.ShootPatterns
{
    public class MultiShootPattern: ShootPattern
    {
        public List<ShootPattern> patterns = new();

        public override void shoot(Shooter shooter)
        {
            for (int i = 0; i < shoots; i++)
            {
                Times.run(() => {
                    foreach (var pattern in patterns)
                    {
                        pattern.shoot(shooter);
                    }
                }, shootInterval);
            }

        }

        public override ShootPattern clone()
        {
            var res = new MultiShootPattern { shoots = shoots, shootInterval = shootInterval};

            foreach (var pattern in patterns)
            {
                res.patterns.Add(pattern.clone());
            }

            return res;
        }
    }
}