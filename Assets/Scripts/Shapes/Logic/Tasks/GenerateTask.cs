using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes.Logic
{
    public class GenerateTask: ScheduleTask
    {
        public GameObject generatePrefab;
        public Vector3 genPosition;
        public Quaternion genRotation;

        public int generates = 1;
        public Vector3 genOffset = new(0, 0, 0);
        public Quaternion genRotOffset = new(0, 0, 0, 1);

        private int generatedCount;

        protected override void begin()
        {
            generatedCount = 0;
        }

        protected override void action()
        {
            var genStep = 1f / generates;

            var prog = duration < 0? interp(Mathf.Clamp01(time)): progress;

            if (prog < generatedCount * genStep) return;
            var inst = Instantiate(generatePrefab,
                genPosition + genOffset*prog,
                Quaternion.Slerp(genRotation, genRotation * genRotOffset, prog)
            );
            inst.SetActive(true);
            generatedCount++;
        }

        protected override void post()
        {
        }
    }
}