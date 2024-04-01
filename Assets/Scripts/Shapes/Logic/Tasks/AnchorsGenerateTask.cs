using UnityEngine;

namespace Shapes.Logic
{
    public class AnchorsGenerateTask: ScheduleTask
    {
        public GameObject generatePrefab;
        public GameObject anchor;
        public int generates = 1;

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
            var inst = Instantiate(generatePrefab, anchor.transform.position, anchor.transform.rotation);
            inst.SetActive(true);
            generatedCount++;
        }

        protected override void post()
        {
        }
    }
}