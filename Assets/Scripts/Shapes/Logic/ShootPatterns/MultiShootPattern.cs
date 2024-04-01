using System.Collections.Generic;
using Shapes.Components;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
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
    }
}