using UnityEngine;

namespace Shapes.Logic
{
    public class AnchorsGenerateTask: GenerateTask
    {
        public GameObject anchor;

        protected override Vector3 genPos(float prog)
        {
            return anchor.transform.position;
        }

        protected override Quaternion genRot(float prog)
        {
            return anchor.transform.rotation;
        }


        public override ScheduleTask clone()
        {
            return new AnchorsGenerateTask { duration = duration, beginTime = beginTime, interp = interp,
                anchor = anchor};
        }
    }
}