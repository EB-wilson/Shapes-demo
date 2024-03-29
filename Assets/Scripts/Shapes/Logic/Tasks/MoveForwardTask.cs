using UnityEngine;

namespace Shapes.Logic
{
    public class MoveForwardTask: Task
    {
        public float moveSpeed;
        public Vector3 moveRot;
        
        protected override void begin()
        {

        }

        protected override void action()
        {
            var trans = transform;
            trans.eulerAngles += moveRot * Time.deltaTime;
            trans.position += trans.forward * (moveSpeed * Time.deltaTime);
        }

        protected override void post()
        {

        }
    }
}