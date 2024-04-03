using UnityEngine;

namespace Shapes.Logic
{
    public class LinearPosGenerateTask: GenerateTask
    {
        public Vector3 genPosition;
        public Quaternion genRotation;

        public Vector3 genOffset = new(0, 0, 0);
        public Quaternion genRotOffset = new(0, 0, 0, 1);

        protected override Vector3 genPos(float prog)
        {
            return genPosition + genOffset * prog;
        }

        protected override Quaternion genRot(float prog)
        {
            return Quaternion.Slerp(genRotation, genRotation * genRotOffset, prog);
        }
    }
}