using UnityEngine;

namespace Shapes.Logic
{
    public class PositionGenerateTask: GenerateTask
    {
        public Vector3[] genPosition;
        public Quaternion[] genRotation;

        protected override Vector3 genPos(float prog)
        {
            return genPosition[generatedCount % genPosition.Length];
        }

        protected override Quaternion genRot(float prog)
        {
            return genRotation[generatedCount % genRotation.Length];
        }

        public override ScheduleTask clone()
        {
            return new PositionGenerateTask { duration = duration, beginTime = beginTime, interp = interp,
                genPosition = genPosition, genRotation = genRotation};
        }
    }
}