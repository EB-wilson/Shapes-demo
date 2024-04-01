using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerStatus : MonoBehaviour
    {
        public float health;
        public float score;
        public float power;

        public float balance = 0.5f;

        private void Awake()
        {
            GlobalVars.player = this;
        }
    }
}
