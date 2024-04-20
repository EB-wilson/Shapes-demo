using UnityEngine;

namespace Shapes.Logic
{
    public class MovementTask: ScheduleTask
    {
        public Vector3 moveVec;
        public Quaternion moveRot;
        public bool noRot;

        private Vector3 beginPos;
        private Quaternion beginRot;

        protected override void begin()
        {
            var trans = self.transform;
            beginPos = trans.position;
            beginRot = trans.rotation;
        }

        protected override void action()
        {
            var trans = self.transform;
            if (!noRot) trans.rotation = Quaternion.Slerp(beginRot, moveRot, progress);
            trans.position = beginPos + moveVec * progress;
        }

        protected override void post()
        {

        }

        public override ScheduleTask clone()
        {
            return new MovementTask{ duration = duration, beginTime = beginTime, interp = interp,
                moveVec = moveVec, moveRot = moveRot, noRot = noRot };
        }
    }
}