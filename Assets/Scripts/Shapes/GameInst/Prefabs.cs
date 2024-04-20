using Shapes.Components;
using UnityEngine;

namespace Shapes.GameInst
{
    public static class Prefabs
    {
        public static readonly ScheduleObject
            fragEnemy = Resources.Load<ScheduleObject>("Prefabs/Entity/Frag-Enemy"),
            fragEnemy1 = Resources.Load<ScheduleObject>("Prefabs/Entity/Frag-Enemy-1"),
            pieceEnemy = Resources.Load<ScheduleObject>("Prefabs/Entity/Piece-Enemy");

        public static readonly Bullet
            ammoMini = Resources.Load<Bullet>("Prefabs/Bullet/AmmoMini"),
            ammoMedium = Resources.Load<Bullet>("Prefabs/Bullet/AmmoMedium"),
            glassBullet = Resources.Load<Bullet>("Prefabs/Bullet/AmmoGlass"),
            triBullet = Resources.Load<Bullet>("Prefabs/Bullet/AmmoTri"),
            ballBullet = Resources.Load<Bullet>("Prefabs/Bullet/AmmoBall"),
            dartBullet = Resources.Load<Bullet>("Prefabs/Bullet/AmmoDart"),
            glassBulletRed = Resources.Load<Bullet>("Prefabs/Bullet/Variant/AmmoGlassRed"),
            triBulletRed = Resources.Load<Bullet>("Prefabs/Bullet/Variant/AmmoTriRed");
    }
}