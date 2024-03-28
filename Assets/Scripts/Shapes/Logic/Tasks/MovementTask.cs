using UnityEngine;

namespace Shapes.Logic
{
    public class MovementTask: Task
    {
        public Vector3 moveVec;
        public Vector3 rotateVec;

        private Vector3 beginPos;
        private Vector3 beginRot;

        private Transform selfTrans;

        protected override void begin()
        {
            // 记录初始位置和旋转
            selfTrans = transform;
            beginPos = selfTrans.position;
            beginRot = selfTrans.eulerAngles;
        }

        protected override void action()
        {
            selfTrans.eulerAngles = beginRot + rotateVec * progress;
            selfTrans.position = beginPos + moveVec * progress;
        }

        protected override void post()
        {

        }
    }
}