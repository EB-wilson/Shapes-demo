using System;
using Shapes.Components;
using Shapes.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes.Logic
{
    /// <summary>
    /// 保存关于世界的一些信息和行为，主要为被动调用逻辑
    /// </summary>
    [RequireComponent(typeof(Schedule))]
    public class World : MonoBehaviour
    {
        public Rect worldBounds;
        public Rect viewPortBounds;
        public Rect viewPort;

        public float playerHeight = 10;
        public float cameraHeight = 20;

        public float boundLen = 3;
        public float clampBound = 50;
        public float velMul = 2;

        [NonSerialized] public Terrain terrain;

        void Awake()
        {
            GlobalVars.world = this;
            terrain = GetComponent<Terrain>();
        }

        void Update()
        {
            terrain.terrainData.size = new Vector3(worldBounds.width, 20, worldBounds.height);
            terrain.transform.position = new Vector3(worldBounds.x, 0, worldBounds.y);
        }

        public void clamp(Controllable2D target)
        {
            var diff = new Vector2(0, 0);

            var pos = target.transform.position;
            if (pos.x < viewPortBounds.xMin) diff.x = viewPortBounds.xMin - pos.x;
            if (pos.x > viewPortBounds.xMax) diff.x = viewPortBounds.xMax - pos.x;
            if (pos.z < viewPortBounds.yMin) diff.y = viewPortBounds.yMin - pos.z;
            if (pos.z > viewPortBounds.yMax) diff.y = viewPortBounds.yMax - pos.z;

            diff /= boundLen/(velMul*target.speed);

            target.motion.hasten(new Vector3(diff.x, 0, diff.y));
        }

        public void checkBullet(Transform obj)
        {
            var pos = obj.position;
            var bound = new Rect(
                viewPortBounds.x - clampBound,
                viewPortBounds.y - clampBound,
                viewPortBounds.width + clampBound*2,
                viewPortBounds.height + clampBound*2
            );
            if (!bound.Contains(new Vector2(pos.x, pos.z)))
            {
                Destroy(obj.gameObject);
            }
        }

        public void syncCamera(Controllable2D player, Camera cam)
        {
            cam.orthographicSize = viewPort.height/2;
            cam.aspect = viewPort.width/viewPort.height;
            cam.fieldOfView = 2.0f * Mathf.Atan(viewPort.height/2/(cameraHeight + GlobalVars.cameraHeightOffset)) * Mathf.Rad2Deg;

            var widthDis = viewPortBounds.width - viewPort.width;
            var heightDis = viewPortBounds.height - viewPort.height;

            var pl = player.transform.position;
            var position = pl;
            var xRate = position.x / viewPortBounds.width;
            var yRate = position.z / viewPortBounds.height;

            var trans = cam.transform;
            var pos = new Vector3(
                viewPortBounds.x + viewPort.width/2 + widthDis * xRate,
                pl.y + cameraHeight + GlobalVars.cameraHeightOffset,
                viewPortBounds.y + viewPort.height/2 + heightDis * yRate
            );
            trans.position = pos;
            viewPort.x = pos.x - viewPort.x/2;
            viewPort.y = pos.z - viewPort.y/2;
        }
    }
}