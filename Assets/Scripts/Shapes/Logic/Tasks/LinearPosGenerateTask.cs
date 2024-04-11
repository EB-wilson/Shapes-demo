using UnityEngine;

namespace Shapes.Logic
{
    public class LinearPosGenerateTask: GenerateTask
    {
        public Vector3 fromPos;
        public Quaternion fromRot;

        public Vector3 toPos;
        public Quaternion toRot = Quaternion.identity;

        protected override Vector3 genPos(float prog)
        {
            return Vector3.Lerp(fromPos, toPos, prog);
        }

        protected override Quaternion genRot(float prog)
        {
            return Quaternion.Slerp(fromRot, toRot, prog);
        }

        public override ScheduleTask clone()
        {
            return new LinearPosGenerateTask{ duration = duration, beginTime = beginTime, interp = interp,
                fromPos = fromPos, fromRot = fromRot, toPos = toPos, toRot = toRot};
        }
    }
}