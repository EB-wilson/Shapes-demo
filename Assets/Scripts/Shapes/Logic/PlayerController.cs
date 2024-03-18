using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    /// <summary>
    /// 默认的键盘自机控制程序，出于STG要求的操作平滑考虑，未采用物理系统进行控制
    /// 包括了所有用户输入的控制逻辑，需要在初始化时提供所有自机的形态和僚机的预制物
    /// </summary>
    [RequireComponent(typeof(Transform), typeof(Controllable2D), typeof(PlayerStatus))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerControllable2D hikari;
        public PlayerControllable2D tairitsu;

        public Camera camera;

        protected PlayerControllable2D cur;
        protected PlayerControllable2D sub;
        protected bool status;

        private Transform selfPos;
        private Controllable2D selfCont;

        //public GameObject wingmanPrefab;
        void Start()
        {
            selfPos = transform;
            selfCont = GetComponent<Controllable2D>();

            cur = Instantiate(tairitsu, selfPos, false);
            sub = Instantiate(hikari, selfPos, false);
        }

        // Update is called once per frame
        void Update()
        {
            sub.gameObject.SetActive(false);
            cur.gameObject.SetActive(true);

            selfCont.speed = cur.speed;
            selfCont.hasten = cur.hasten;
            selfCont.drag = cur.drag;

            Vector2 v = new Vector2();

            if (Input.GetKey(Bindings.right)) { v += new Vector2(1, 0); }
            if (Input.GetKey(Bindings.left)) { v -= new Vector2(1, 0); }
            if (Input.GetKey(Bindings.down)) { v -= new Vector2(0, 1); }
            if (Input.GetKey(Bindings.up)) { v += new Vector2(0, 1); }

            if (v.x != 0 || v.y != 0) selfCont.move(Math.angle(v.x, v.y));

            if (GlobalVars.switchStatToggle)
            {
                if (Input.GetKeyDown(Bindings.switchStatus))
                {
                    status = !status;
                    switchStatus();
                }
            }
            else
            {
                if (Input.GetKeyDown(Bindings.switchStatus) && !status)
                {
                    status = true;
                    switchStatus();
                }
                else if (Input.GetKeyUp(Bindings.switchStatus) && status)
                {
                    status = false;
                    switchStatus();
                }
            }

            if (Input.GetKeyDown(Bindings.fire))
            {
                cur.shooter.shooting = true;
                sub.shooter.shooting = true;
            }

            if (Input.GetKeyUp(Bindings.fire))
            {
                cur.shooter.shooting = false;
                sub.shooter.shooting = false;
            }

            GlobalVars.world.clamp(selfCont);
            GlobalVars.world.syncCamera(selfCont, camera);
        }

        private void switchStatus()
        {
            (cur, sub) = (sub, cur);
        }
    }

}
