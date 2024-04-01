using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public class Logic : MonoBehaviour
    {
        public Camera camera;

        void Update()
        {
            camera.orthographic = GlobalVars.isOrthographicView;

            Times.updateTask();
        }
    }
}