using UnityEngine;
using UnityEngine.Serialization;

namespace Shapes.Logic
{
    public class MoveForwardTask: ScheduleTask
    {
        public float moveSpeed;
        public Vector3 moveRot;
        
        protected override void begin()
        {

        }

        protected override void action()
        {
            var trans = self.transform;
            trans.eulerAngles += moveRot * Time.deltaTime;
            trans.position += trans.forward * (moveSpeed * Time.deltaTime);
        }

        protected override void post()
        {

        }

        public override ScheduleTask clone()
        {
            return new MoveForwardTask{ duration = duration, beginTime = beginTime, interp = interp,
                moveSpeed = moveSpeed, moveRot = moveRot};
        }
    }
}