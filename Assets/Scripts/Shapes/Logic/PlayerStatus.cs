using System;
using Shapes.Utils;
using UnityEngine;

namespace Shapes.Components
{
    public class PlayerStatus : MonoBehaviour
    {
        public float maxHealth = 200;

        public float health = 100;
        public long score;
        public float power;
        public int bombs;
        public int miss;
        public int graze;

        public float balance = 0.5f;

        private void Awake()
        {
            GlobalVars.player = this;
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        public float damageBalance(bool shift)
        {
            return shift ? balance : -balance;
        }
    }
}
