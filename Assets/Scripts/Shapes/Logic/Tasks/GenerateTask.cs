using UnityEngine;

namespace Shapes.Logic
{
    public class GenerateTask: Task
    {
        public GameObject generatePrefab;
        public Vector3 position;
        public Quaternion rotation;

        public int generates = 1;
        public Vector3 genOffset = new(0, 0, 0);

        private int generatedCount;

        protected override void begin()
        {
            generatedCount = 0;
        }

        protected override void action()
        {
            var genStep = 1f / generates;

            var prog = duration < 0? Mathf.Clamp01(time): progress;

            if (prog < generatedCount * genStep) return;
            Instantiate(generatePrefab, position + genOffset*generatedCount, rotation);
            generatedCount++;
        }

        protected override void post()
        {
        }
    }
}