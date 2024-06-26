using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Logic
{
    public class Logic : MonoBehaviour
    {
        public Camera currCamera;

        void Update()
        {
            currCamera.orthographic = GlobalVars.isOrthographicView;

            Times.updateTask();
        }
    }
}