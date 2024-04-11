using Shapes.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes.Logic
{
    public abstract class GenerateTask: ScheduleTask
    {
        public ScheduleObject[] generatePrefabList;
        public int[] generateIndex;
        public int generates = 1;
        public int generateBatch = 1;

        protected int generatedCount;

        protected abstract Vector3 genPos(float prog);
        protected abstract Quaternion genRot(float prog);

        protected override void begin()
        {
            generatedCount = 0;
        }

        protected override void action()
        {
            var genStep = 1f / generates;

            var prog = duration < 0? interp(Mathf.Clamp01(time)): progress;

            if (prog < generatedCount * genStep) return;

            for (var j = 0; j < generateBatch; j++)
            {
                var i = generateIndex == null? j: generateIndex[generatedCount % generateIndex.Length];
                generate(generatePrefabList[i % generatePrefabList.Length], genPos(prog), genRot(prog));
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void generate(ScheduleObject gen, Vector3 pos, Quaternion rot)
        {
            var inst = gen.makeInst(pos, rot).gameObject;
            inst.SetActive(true);
            generatedCount++;
        }

        protected override void post() { }
    }
}