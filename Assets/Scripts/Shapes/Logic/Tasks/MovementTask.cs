using UnityEngine;

namespace Shapes.Logic
{
    public class MovementTask: ScheduleTask
    {
        public Vector3 moveVec;
        public Quaternion moveRot;

        private Vector3 beginPos;
        private Quaternion beginRot;

        protected override void begin()
        {
            var trans = transform;
            beginPos = trans.position;
            beginRot = trans.rotation;
        }

        protected override void action()
        {
            var trans = transform;
            trans.rotation = Quaternion.Lerp(beginRot, moveRot, progress);
            trans.position = beginPos + moveVec * progress;
        }

        protected override void post()
        {

        }
    }
}